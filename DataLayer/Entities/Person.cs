using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using DataLayer.Entities.Common;
using DataLayer.Entities.ManyToManyHelper;
using DataLayer.Extensions;

namespace DataLayer.Entities
{
    public class Person : EntityBase
    {
        public Person()
        {
            NeuralNetworks =
                new JoinCollectionFacade<NeuralNetwork, Person, NeuralNetworkPerson>(this, NeuralNetworkPeople);
        }

        public string Name { get; set; }
        public string Description { get; set; }

        public IEnumerable<PersonImage> Images { get; set; }

        private ICollection<NeuralNetworkPerson> NeuralNetworkPeople { get; } = new List<NeuralNetworkPerson>();

        [NotMapped] public IEnumerable<NeuralNetwork> NeuralNetworks { get; }
    }
}