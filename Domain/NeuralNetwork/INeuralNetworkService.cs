using System.Threading.Tasks;

namespace Domain.NeuralNetwork
{
    public interface INeuralNetworkService
    {
        Task GetAll();
        int Create(string neuralNetworkName, string peopleIds);
    }
}