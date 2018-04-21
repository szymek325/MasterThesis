using System.IO;

namespace DropboxIntegration.Files.DTO
{
    public class FileToUpload
    {
        public string FileName;
        public Stream FileStream;
    }
}