using DataLayer.Entities;
using DataLayer.Repositories.Base;
using DataLayer.Repositories.Interface;

namespace DataLayer.Repositories.Implementation
{
    public class NeuralNetworkRepository : GenericRepository<NeuralNetwork>,
        INeuralNetworkRepository
    {
        public NeuralNetworkRepository(MasterContext context) : base(context)
        {
        }
    }
}