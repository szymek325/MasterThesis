using System;
using System.Collections.Generic;
using Domain.Files.DTO;
using Domain.NeuralNetwork.DTO;

namespace Domain.FaceRecognition.DTO
{
    public class RecognitionRequest
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Status { get; set; }
        public string Thumbnail { get; set; }
        public DateTime CreationTime { get; set; }
        public DateTime? CompletionTime { get; set; }
        public FileLink FileLink { get; set; }
        public IEnumerable<RecognitionResultOutput> Results { get; set; }
        public string NeuralNetworkInfo { get; set; }
    }
}