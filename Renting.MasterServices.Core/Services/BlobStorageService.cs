using log4net;
using Renting.MasterServices.Core.Interfaces;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Microsoft.WindowsAzure.Storage;
using Microsoft.WindowsAzure.Storage.Blob;
using Renting.MasterServices.Domain;
using Renting.MasterServices.Core.Dtos;
using System.Net;

namespace Renting.MasterServices.Core.Services
{
    /// <summary>
    /// Servicio que permite gestionar las imagenes al blob storage
    /// </summary>
    public class BlobStorageService : IBlobStorageService
    {
        private readonly ILog log;
        private readonly IConfigProvider config;

        /// <summary>
        /// Cadena de conexión del blob storage
        /// </summary>
        public string StorageConnectionString { get; set; }
        
        /// <summary>
        /// Nombre del contenedor del blob storage
        /// </summary>
        public string ContainerName { get; set; }

        public BlobStorageService(ILog log, IConfigProvider config)
        {
            this.log = log;
            this.config = config;
        }

        /// <summary>
        /// Guarda un archivo en el blob storage
        /// </summary>
        /// <param name="formFile">Archivo que se guarda en el blob storage</param>
        /// <returns></returns>
        public async Task<string> SaveFileAsync(UploadFile formFile)
        {
            CloudBlobContainer cloudBlobContainer = null;

            try
            {
                CloudStorageAccount storageAccount = CloudStorageAccount.Parse(StorageConnectionString);
                CloudBlobClient cloudBlobClient = storageAccount.CreateCloudBlobClient();
                cloudBlobContainer = cloudBlobClient.GetContainerReference(ContainerName);
                CloudBlockBlob cloudBlockBlob = cloudBlobContainer.GetBlockBlobReference(formFile.FileName);
                await cloudBlockBlob.UploadFromByteArrayAsync(formFile.ByteArray,0, formFile.ByteArray.Length).ConfigureAwait(false);
                return cloudBlockBlob.Uri.AbsoluteUri;
            }
            catch (Exception ex)
            {
                log.Error(ex.Message, ex);
                throw;
            }
        }

        /// <summary>
        /// Elimina un archivo del blob storage
        /// </summary>
        /// <param name="blobName">Nombre del archivo a eliminar</param>
        /// <returns></returns>
        public async Task<bool> DeleteFileAsync(string blobName)
        {
            CloudBlobContainer cloudBlobContainer = null;

            try
            {
                CloudStorageAccount storageAccount = CloudStorageAccount.Parse(StorageConnectionString);
                CloudBlobClient cloudBlobClient = storageAccount.CreateCloudBlobClient();
                cloudBlobContainer = cloudBlobClient.GetContainerReference(ContainerName);
                CloudBlockBlob cloudBlockBlob = cloudBlobContainer.GetBlockBlobReference(blobName);
                return await cloudBlockBlob.DeleteIfExistsAsync().ConfigureAwait(false);
            }
            catch (Exception ex)
            {
                log.Error(ex.Message, ex);
                throw;
            }
        }
    }
}
