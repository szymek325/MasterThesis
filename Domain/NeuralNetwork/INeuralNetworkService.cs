using System.Collections.Generic;
using System.Threading.Tasks;
using Domain.NeuralNetwork.DTO;

namespace Domain.NeuralNetwork
{
    public interface INeuralNetworkService
    {
        Task<IEnumerable<NeuralNetworkRequest>> GetAll();
        Task<int> Create(string neuralNetworkName, string peopleIds);
        Task<NeuralNetworkRequest> GetById(int id);
        Task<IEnumerable<NeuralNetworkBaseInfoOutput>> GetAllCompleted();
    }
}