namespace DataLayer.Entities
{
    public class NeuralNetworkPerson
    {
        public int NeuralNetworkId { get; set; }
        public int PersonId { get; set; }
        public NeuralNetwork NeuralNetwork { get; set; }
        public Person Person { get; set; }
    }
}