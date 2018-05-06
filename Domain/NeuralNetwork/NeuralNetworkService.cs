using System.Threading.Tasks;
using DataLayer.Repositories.Interface;

namespace Domain.NeuralNetwork
{
    public class NeuralNetworkService : INeuralNetworkService
    {
        private readonly INeuralNetworkRepository nnRepo;
        private readonly IPersonRepository personRepo;

        public NeuralNetworkService(INeuralNetworkRepository nnRepo, IPersonRepository personRepo)
        {
            this.nnRepo = nnRepo;
            this.personRepo = personRepo;
        }

        public Task Create()
        {
            var person = personRepo.GetAllPeople();
            var neural = new DataLayer.Entities.NeuralNetwork
            {
                Name = "1",
            };
            throw new System.NotImplementedException();
        }
    }
}