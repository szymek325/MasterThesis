using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using DataLayer.Entities.Common;
using DataLayer.Entities.ManyToManyHelper;
using DataLayer.Extensions;

namespace DataLayer.Entities
{
    public class NeuralNetwork : EntityBase
    {
        public NeuralNetwork()
        {
            People = new JoinCollectionFacade<Person, NeuralNetwork, NeuralNetworkPerson>(this, NeuralNetworkPeople);
        }

        public string Name { get; set; }
        public string Description { get; set; }
        public int MaxNumberOfPhotosPerPerson { get; set; }
        public int? StatusId { get; set; }
        public Status Status { get; set; }
        public DateTime? CompletionTime { get; set; }
        public IEnumerable<NeuralNetworkFile> Files { get; set; }
        private ICollection<NeuralNetworkPerson> NeuralNetworkPeople { get; } = new List<NeuralNetworkPerson>();

        [NotMapped]
        public ICollection<Person> People { get; }
    }
}