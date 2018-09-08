using System.Collections.Generic;
using System.Threading.Tasks;
using Domain.NeuralNetwork.DTO;

namespace Domain.NeuralNetwork
{
    public interface INeuralNetworkService
    {
        Task<IEnumerable<AllNeuralNetworksOutput>> GetAll();
        Task<int> Create(string neuralNetworkName, string peopleIds,int photosToTake);
        Task<NeuralNetworkRequest> GetById(int id);
        Task<IEnumerable<NeuralNetworkBaseInfoOutput>> GetAllCompleted();
    }
}