using DataLayer.Extensions;

namespace DataLayer.Entities.ManyToManyHelper
{
    public class NeuralNetworkPerson : IJoinEntity<NeuralNetwork>, IJoinEntity<Person>
    {
        public int NeuralNetworkId { get; set; }
        public int PersonId { get; set; }
        public NeuralNetwork NeuralNetwork { get; set; }
        public Person Person { get; set; }

        NeuralNetwork IJoinEntity<NeuralNetwork>.Navigation
        {
            get => NeuralNetwork;
            set => NeuralNetwork = value;
        }

        Person IJoinEntity<Person>.Navigation
        {
            get => Person;
            set => Person = value;
        }
    }
}