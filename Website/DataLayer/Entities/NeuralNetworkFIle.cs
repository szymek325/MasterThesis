﻿using System;
using DataLayer.Entities.Common;

namespace DataLayer.Entities
{
    public class NeuralNetworkFile : EntityBase
    {
        public string Name { get; set; }
        public int NeuralNetworkId { get; set; }
        public NeuralNetwork NeuralNetwork { get; set; }
        public int NeuralNetworkTypeId { get; set; }
        public NeuralNetworkType NeuralNetworkType { get; set; }
        public string AdditionalData { get; set; }
        public string TrainingTime { get; set; }
        public string ProcessingTime { get; set; }
        public string FileSize { get; set; }
    }
}