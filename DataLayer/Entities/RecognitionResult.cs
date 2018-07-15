using DataLayer.Entities.Common;

namespace DataLayer.Entities
{
    public class RecognitionResult : EntityBase
    {
        public int IdentifiedPersonId { get; set; }
        public int Confidence { get; set; }
        public int NeuralNetworkFileId { get; set; }
        public NeuralNetworkFile NeuralNetworkFile { get; set; }
    }
}