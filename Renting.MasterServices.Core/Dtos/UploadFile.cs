using System.IO;

namespace Renting.MasterServices.Core.Dtos
{
    public class UploadFile
    {
        public string FileName { get; set; }
        public byte[] ByteArray { get; set; }
    }
}
