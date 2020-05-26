using Renting.MasterServices.Core.Dtos;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Renting.MasterServices.Core.Interfaces
{
    public interface IBlobStorageService
    {
        string StorageConnectionString { get; set; }
        string ContainerName { get; set; }
        Task<string> SaveFileAsync(UploadFile formFile);
        Task<bool> DeleteFileAsync(string blobName);
    }
}
