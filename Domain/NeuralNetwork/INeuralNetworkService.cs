using System.Collections.Generic;
using System.Threading.Tasks;
using Domain.NeuralNetwork.DTO;

namespace Domain.NeuralNetwork
{
    public interface INeuralNetworkService
    {
        IEnumerable<NeuralNetworkRequest> GetAll();
        int Create(string neuralNetworkName, string peopleIds);
        NeuralNetworkRequest GetById(int id);
    }
}