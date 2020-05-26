using Microsoft.AspNetCore.Http;
using System.IO;

namespace Renting.MasterServices.Core.Interfaces
{
    /// <summary>
    /// 
    /// </summary>
    public interface IFileProcesingService
    {
        bool ValidateFileExtension(IFormFile file);
        bool ValidateFileSize(IFormFile file);
        string GetFileName(IFormFile file);
        byte[] GetFileByteArrayFromStreamImage(Stream fileStream);
        bool ValidateImageDimenensions(IFormFile file);
    }
}
