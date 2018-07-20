using System.Collections.Generic;
using DataLayer.Entities;
using DataLayer.Repositories.Base;

namespace DataLayer.Repositories.Interface
{
    public interface INeuralNetworkRepository : IGenericRepository<NeuralNetwork>
    {
        IEnumerable<NeuralNetwork> GetAllNeuralNetworksWithDependencies();
        IEnumerable<NeuralNetwork> GetAllCompleted();
        NeuralNetwork GetById(int id);
    }
}