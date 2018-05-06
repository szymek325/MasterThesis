using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace DataLayer.Entities
{
    public class Person : EntityBase
    {
        public string Name { get; set; }
        public string Description { get; set; }

        [Required]
        public string Guid { get; set; }

        public IEnumerable<File> Files { get; set; }
        public IEnumerable<NeuralNetworkPerson> NeuralNetworkPeople { get; set; }
    }
}