using System;
using Domain.Files.DTO;
using Domain.NeuralNetwork.DTO;

namespace Domain.FaceRecognition.DTO
{
    public class FaceRecoRequest
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Status { get; set; }
        public string Thumbnail { get; set; }
        public NeuralNetworkOutput NeuralNetwork { get; set; }
        public DateTime CreationTime { get; set; }
        public FileLink FileLink { get; set; }
    }
}