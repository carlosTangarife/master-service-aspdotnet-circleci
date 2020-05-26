using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Configuration;
using Renting.MasterServices.Core.Interfaces;
using Renting.MasterServices.Domain;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Imaging;
using System.IO;
using System.Linq;

namespace Renting.MasterServices.Core.Services
{
    /// <summary>
    /// Clase que procesa las imagenes subidas al blob storage
    /// </summary>
    public class FileProcesingService : IFileProcesingService
    {
        private readonly IConfigProvider config;
        private const int MIN_INTERVAL_DIMENSION = 10;
        private const int MAX_INTERVAL_DIMENSION = 10;

        public FileProcesingService(IConfigProvider config)
        {
            this.config = config;
        }

        /// <summary>
        /// Valida las extensiones permitidas de un archivo
        /// </summary>
        /// <param name="fileExtension">Extensión del archivo</param>
        /// <returns></returns>
        public bool ValidateFileExtension(IFormFile file)
        {
            string fileExtension = Path.GetExtension(file.FileName);
            var allowedFileExtensions = config.GetVal("Appsettings:AllowedFileExtensions");
            return allowedFileExtensions.Contains(fileExtension.ToLower());
        }

        /// <summary>
        /// Valida el tamaño del archivo
        /// </summary>
        /// <param name="file">Archivo a validar</param>
        /// <returns></returns>
        public bool ValidateFileSize(IFormFile file)
        {
            var fileSize = config.GetVal("Appsettings:MaxAllowedFileSize");
            return file.Length <= Convert.ToInt64(fileSize);
        }

        /// <summary>
        /// Obtiene el nombre del archivo
        /// </summary>
        /// <param name="file">Archivo</param>
        /// <returns></returns>
        public string GetFileName(IFormFile file)
        {
            string fileExtension = Path.GetExtension(file.FileName);
            return $"{ Guid.NewGuid() }{ fileExtension}";
        }

        /// <summary>
        /// Obtiene el array de bytes de la secuencia de una imagen
        /// </summary>
        /// <param name="fileStream"></param>
        /// <returns></returns>
        public byte[] GetFileByteArrayFromStreamImage(Stream fileStream)
        {
            byte[] byteArray;
            var image = GetThumbnailImage(fileStream);

            using (MemoryStream imageStream = new MemoryStream())
            {
                image.Save(imageStream, ImageFormat.Png);
                imageStream.Seek(0, 0);
                byteArray = imageStream.ToArray();
            }

            return byteArray;
        }

        /// <summary>
        /// Valida las dimensiones de la imagen
        /// </summary>
        /// <param name="file">Imagen</param>
        /// <returns></returns>
        public bool ValidateImageDimenensions(IFormFile file)
        {
            var image = Image.FromStream(file.OpenReadStream(), true, true);
            var imageDimensionsConfig = config.GetSection("Appsettings:AllowedImageDimensions:Dimensions")
                                .GetChildren()
                                .Select(x => new
                                {
                                    width = x.GetValue<int>("Width"),
                                    height = x.GetValue<int>("Height")
                                });

            if (!imageDimensionsConfig.Any())
            {
                ImageDispose(image);
                return true;
            }

            foreach (var item in imageDimensionsConfig)
            {
                if (image.Width.IsBetween(item.width - MIN_INTERVAL_DIMENSION,
                    item.width + MAX_INTERVAL_DIMENSION)
                    && image.Height.IsBetween(item.height - MIN_INTERVAL_DIMENSION, 
                    item.height + MAX_INTERVAL_DIMENSION))
                {
                    ImageDispose(image);
                    return true;
                }
            }

            ImageDispose(image);
            return false;
        }

        private void ImageDispose(Image image)
        {
            image.Dispose();
        }

        /// <summary>
        /// Obtiene la miniatura de la secuencia de una imagen
        /// </summary>
        /// <param name="fileStream"></param>
        /// <returns></returns>
        private Image GetThumbnailImage(Stream fileStream)
        {
            var originalImage = Image.FromStream(fileStream, true, true);
            Size thumbnailSize = GetThumbnailSize(originalImage);
            return originalImage.GetThumbnailImage(thumbnailSize.Width, thumbnailSize.Height, null, IntPtr.Zero);
        }

        /// <summary>
        /// Obtiene el tamño para la imagen miniatura
        /// </summary>
        /// <param name="originalImage"></param>
        /// <returns></returns>
        private Size GetThumbnailSize(Image originalImage)
        {
            int.TryParse(config.GetVal("Appsettings:MaxFileResizeSize"), out int maxPixels);
            int originalWidth = originalImage.Width;
            int originalHeight = originalImage.Height;

            double factor;
            if (originalWidth > originalHeight)
            {
                factor = (double)maxPixels / originalWidth;
            }
            else
            {
                factor = (double)maxPixels / originalHeight;
            }

            return new Size((int)(originalWidth * factor), (int)(originalHeight * factor));
        }
    }
}
