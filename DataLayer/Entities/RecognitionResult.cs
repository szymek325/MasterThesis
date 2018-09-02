using System;
using DataLayer.Entities.Common;

namespace DataLayer.Entities
{
    public class RecognitionResult : EntityBase
    {
        public int IdentifiedPersonId { get; set; }
        public float Confidence { get; set; }
        public int NeuralNetworkFileId { get; set; }
        public NeuralNetworkFile NeuralNetworkFile { get; set; }
        public int RecognitionId { get; set; }
        public Recognition Recognition { get; set; }
        public string Comments { get; set; }
        public DateTime? ProcessingTime { get; set; }
    }
}