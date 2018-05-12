using System;
using System.Collections.Generic;
using System.Text;
using Domain.Files.DTO;

namespace Domain.FaceRecognition.DTO
{
    public class NewRequest
    {
        public string Name { get; set; }
        public int NeuralNetworkId { get; set; }
        public IEnumerable<FileToUpload> Files { get; set; }
    }
}
