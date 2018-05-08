using System.Collections.Generic;
using System.Threading.Tasks;
using Domain.NeuralNetwork.DTO;

namespace Domain.NeuralNetwork
{
    public interface INeuralNetworkService
    {
        IEnumerable<NeuralNetworkOutput> GetAll();
        int Create(string neuralNetworkName, string peopleIds);
        NeuralNetworkOutput GetById(int id);
    }
}