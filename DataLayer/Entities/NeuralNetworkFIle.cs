using System;
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
        public DateTime? TrainingTime { get; set; }
        public DateTime? ProcessingTime { get; set; }
    }
}