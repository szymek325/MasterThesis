using System;
using System.Collections.Generic;
using DataLayer.Entities.Common;

namespace DataLayer.Entities
{
    public class Recognition : EntityBase
    {
        public string Name { get; set; }
        public string Description { get; set; }
        public int? NeuralNetworkId { get; set; }
        public NeuralNetwork NeuralNetwork { get; set; }
        public int? StatusId { get; set; }
        public Status Status { get; set; }
        public DateTime? CompletionTime { get; set; }
        public IEnumerable<RecognitionImage> Images { get; set; }
        public IEnumerable<RecognitionResult> RecognitionResults { get; set; }
    }
}