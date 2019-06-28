using System;

namespace Domain.NeuralNetwork.DTO
{
    public class AllNeuralNetworksOutput
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string StatusName { get; set; }
        public DateTime CreationTime { get; set; }
        public DateTime? CompletionTime { get; set; }
        public int PeopleCount { get; set; }
        public int FilesCount { get; set; }
    }
}