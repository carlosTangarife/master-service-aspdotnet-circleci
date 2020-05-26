using AutoMapper;
using log4net;
using Microsoft.AspNetCore.Http;
using Renting.MasterServices.Core.Dtos;
using Renting.MasterServices.Core.Dtos.Provider;
using Renting.MasterServices.Core.Interfaces;
using Renting.MasterServices.Core.Interfaces.Provider;
using Renting.MasterServices.Domain;
using Renting.MasterServices.Domain.Entities.Provider;
using Renting.MasterServices.Domain.IRepository.Provider;
using Renting.MasterServices.Infraestructure.CustomExceptions;
using System;
using System.IO;
using System.Threading.Tasks;
using static Renting.MasterServices.Infraestructure.Enums;

namespace Renting.MasterServices.Core.Services.Provider
{
    /// <summary>
    /// Servicio que administra los comunicados
    /// </summary>
    public class AnnouncementService : Service<Announcement, AnnouncementDto>, IAnnouncementService
    {
        private readonly IAnnouncementRepository announcementRepository;
        private readonly IBlobStorageService blobStorageService;
        private readonly IMapper mapper;
        private readonly IConfigProvider config;
        private readonly IFileProcesingService fileProcesingService;

        public AnnouncementService(IAnnouncementRepository announcementRepository,
            IBlobStorageService blobStorageService,
            IFileProcesingService fileProcesingService,
            IConfigProvider config,
            IMapper mapper,
            ILog log) : base(announcementRepository, log, mapper)
        {
            this.announcementRepository = announcementRepository;
            this.blobStorageService = blobStorageService;
            this.fileProcesingService = fileProcesingService;
            this.mapper = mapper;
            this.config = config;
        }

        /// <summary>
        /// Crear un comunicado
        /// </summary>
        /// <param name="announcement">Objeto que contiene la información del comunicado</param>
        /// <param name="file">Imagen del comunicado</param>
        /// <returns></returns>
        public async Task CreateAnnouncementAsync(AnnouncementDto announcement, IFormFile file)
        {
            Announcement announcementEntity = await ManageImageBlobAsync(announcement, file, AnnouncementAction.Save).ConfigureAwait(false);
            await announcementRepository.AddAsync(announcementEntity).ConfigureAwait(false);
        }

        /// <summary>
        /// Actualiza el comunicado
        /// </summary>
        /// <param name="announcement">Objeto que contiene la información del comunicado</param>
        /// <param name="file">Imagen del comunicado</param>
        /// <returns></returns>
        public async Task UpdateAnnouncementAsync(AnnouncementDto announcement, IFormFile file)
        {
            Announcement announcementEntity = await ManageImageBlobAsync(announcement, file, AnnouncementAction.Update).ConfigureAwait(false);
            await announcementRepository.UpdateAsync(announcementEntity).ConfigureAwait(false);
        }

        /// <summary>
        /// Actualiza el estado del comunicado 
        /// </summary>
        /// <param name="id">Identificador del comunicado</param>
        /// <param name="state">Estado del comunicado</param>
        /// <returns></returns>
        public async Task UpdateAnnouncementStateAsync(int? id, bool state)
        {
            Announcement announcement = ValidateAnnouncementId(id);
            announcement.State = state;
            await announcementRepository.UpdateAsync(announcement).ConfigureAwait(false);
        }

        /// <summary>
        /// Elimina el comunicado
        /// </summary>
        /// <param name="id">Identificador del comunicado</param>
        /// <returns></returns>
        public async Task DeleteAnnouncementAsync(int? id)
        {
            Announcement announcement = ValidateAnnouncementId(id);
            await announcementRepository.DeleteAsync(announcement).ConfigureAwait(false);
            await DeleteFileBlobAsync(announcement.ImageId).ConfigureAwait(false);
            await DeleteFileBlobAsync(announcement.ThumbnailImageId).ConfigureAwait(false);
        }

        /// <summary>
        /// Administra la carga de imagenes al blob
        /// </summary>
        /// <param name="announcement">Objeto que contiene la información del comunicado</param>
        /// <param name="file">Imagen del comunicado</param>
        /// <param name="action"> Define si es creación o actualización de la imagen</param>
        /// <returns></returns>
        private async Task<Announcement> ManageImageBlobAsync(AnnouncementDto announcement, IFormFile file, AnnouncementAction action)
        {
            if (!fileProcesingService.ValidateFileExtension(file))
            {
                throw new InvalidFileExtensionException("La extensión del archivo no está permitida");
            }

            if (!fileProcesingService.ValidateFileSize(file))
            {
                throw new InvalidFileSizeException("El tamaño del archivo excede el máximo permitido");
            }

            if (!fileProcesingService.ValidateImageDimenensions(file))
            {
                throw new InvalidFileDimensionException("Las dimensiones del archivo exceden el máximo permitido");
            }

            string imageName = fileProcesingService.GetFileName(file);
            var imageStream = file.OpenReadStream();

            string thumbnailImageName = fileProcesingService.GetFileName(file);
            byte[] thumbnailImageByteArray = fileProcesingService.GetFileByteArrayFromStreamImage(imageStream);

            if (action == AnnouncementAction.Update)
            {
                await DeleteFileBlobAsync(announcement.ImageId).ConfigureAwait(false);
                await DeleteFileBlobAsync(announcement.ThumbnailImageId).ConfigureAwait(false);
            }

            string urlImage = await SaveFileToBlobAsync(imageName, file.OpenReadStream().GetAllBytes()).ConfigureAwait(false);
            string urlThumbnailImage = await SaveFileToBlobAsync(thumbnailImageName, thumbnailImageByteArray).ConfigureAwait(false);

            announcement.UrlImage = urlImage;
            announcement.UrlThumbnailImage = urlThumbnailImage;
            announcement.ImageId = imageName;
            announcement.ThumbnailImageId = thumbnailImageName;

            return mapper.Map<Announcement>(announcement);
        }

        /// <summary>
        /// Permite guardar la imagen al blob storage
        /// </summary>
        /// <param name="imageName">Nombre de la imagen</param>
        /// <param name="fileByteArray">Array de bytes de la imagen</param>
        /// <returns></returns>
        private async Task<string> SaveFileToBlobAsync(string imageName, byte[] fileByteArray)
        {
            blobStorageService.StorageConnectionString = config.GetVal("Blob:StorageConnection");
            blobStorageService.ContainerName = config.GetVal("Blob:BlobContainerProvider");

            var uploadImage = new UploadFile
            {
                FileName = imageName,
                ByteArray = fileByteArray
            };

            return await blobStorageService.SaveFileAsync(uploadImage).ConfigureAwait(false);
        }

        /// <summary>
        /// Permite borrar una imagen del blob storage
        /// </summary>
        /// <param name="fileName">Nombre del archivo</param>
        /// <returns></returns>
        private async Task DeleteFileBlobAsync(string fileName)
        {
            blobStorageService.StorageConnectionString = config.GetVal("Blob:StorageConnection");
            blobStorageService.ContainerName = config.GetVal("Blob:BlobContainerProvider");

            await blobStorageService.DeleteFileAsync(fileName).ConfigureAwait(false);
        }

        /// <summary>
        /// Valida si el identificador del comunicado existe en la base de datos
        /// </summary>
        /// <param name="id">Identificador del comunicado</param>
        /// <returns></returns>
        private Announcement ValidateAnnouncementId(int? id)
        {
            var announcement = announcementRepository.FindById(id);
            if (announcement == null)
            {
                throw new ArgumentNullException($"El identificador del comunicado: {id} no existe en la base de datos");
            }

            return announcement;
        }
    }
}
