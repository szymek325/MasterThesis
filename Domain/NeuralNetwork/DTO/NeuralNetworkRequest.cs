using System;
using System.Collections.Generic;
using Domain.People.DTO;

namespace Domain.NeuralNetwork.DTO
{
    public class NeuralNetworkRequest
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Status { get; set; }
        public DateTime CreationTime { get; set; }
        public DateTime? CompletionTime { get; set; }
        public IEnumerable<PersonOutput> People { get; set; }
        public IEnumerable<NeuralNetworkFileOutput> Files { get; set; }
    }
}