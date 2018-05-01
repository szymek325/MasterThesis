using System.Collections.Generic;
using Domain.Files.DTO;

namespace Domain.FaceDetection.DTO
{
    public class NewRequest
    {
        public string Name { get; set; }
        public IEnumerable<FileToUpload> Files { get; set; }
    }
}