using System.Collections.Generic;
using Domain.NeuralNetwork.DTO;

namespace WebRazor.Models.NeuralNetworks
{
    public class NeuralNetworksViewModel
    {
        public IEnumerable<AllNeuralNetworksOutput> NeuralNetworks { get; set; }
    }
}