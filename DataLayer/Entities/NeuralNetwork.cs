using System.Collections.Generic;

namespace DataLayer.Entities
{
    public class NeuralNetwork : EntityBase
    {
        public string Name { get; set; }
        public string Description { get; set; }
        public IEnumerable<NeuralNetworkPerson> NeuralNetworkRequestPeople { get; set; }
    }
}