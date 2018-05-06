using DataLayer.Extensions;

namespace DataLayer.Entities
{
    public class NeuralNetworkPerson : IJoinEntity<NeuralNetwork>, IJoinEntity<Person>
    {
        private NeuralNetwork navigation;
        private Person navigation1;
        public int NeuralNetworkId { get; set; }
        public int PersonId { get; set; }
        public NeuralNetwork NeuralNetwork { get; set; }
        public Person Person { get; set; }

        NeuralNetwork IJoinEntity<NeuralNetwork>.Navigation
        {
            get => navigation;
            set => navigation = value;
        }

        Person IJoinEntity<Person>.Navigation
        {
            get => navigation1;
            set => navigation1 = value;
        }
    }
}