﻿namespace Domain.FaceRecognition.DTO
{
    public class RecognitionResultOutput
    {
        public int IdentifiedPersonId { get; set; }
        public float Confidence { get; set; }
        public string NeuralNetworkFileName { get; set; }
        public string NeuralNetworkTypeName { get; set; }
        public string Comments { get; set; }
        public string ProcessingTime { get; set; }
    }
}