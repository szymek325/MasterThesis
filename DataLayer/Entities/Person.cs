using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
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

        [Required]
        public string Guid { get; set; }

        public IEnumerable<File> Files { get; set; }

        private ICollection<NeuralNetworkPerson> NeuralNetworkPeople { get; } = new List<NeuralNetworkPerson>();

        [NotMapped]
        public IEnumerable<NeuralNetwork> NeuralNetworks { get; }
    }
}