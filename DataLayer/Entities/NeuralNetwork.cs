using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using DataLayer.Extensions;

namespace DataLayer.Entities
{
    public class NeuralNetwork : EntityBase
    {
        public NeuralNetwork()
        {
            People = new JoinCollectionFacade<Person, NeuralNetwork, NeuralNetworkPerson>(this,NeuralNetworkPeople);
        }

        public string Name { get; set; }
        public string Description { get; set; }
        private ICollection<NeuralNetworkPerson> NeuralNetworkPeople { get; }= new List<NeuralNetworkPerson>();

        [NotMapped]
        public ICollection<Person> People { get; }
    }
}