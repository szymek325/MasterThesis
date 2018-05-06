using System.Collections.Generic;

namespace DataLayer.Entities
{
    public class NeuralNetworkRequest : EntityBase
    {
        public string Name { get; set; }
        public string Description { get; set; }
        public IEnumerable<NeuralNetworkRequestPerson> NeuralNetworkRequestPeople { get; set; }
    }
}