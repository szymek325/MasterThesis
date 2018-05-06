namespace DataLayer.Entities
{
    public class NeuralNetworkRequestPerson
    {
        public int NeuralNetworkRequestId { get; set; }
        public int PersonId { get; set; }
        public NeuralNetworkRequest NeuralNetworkRequest { get; set; }
        public Person Person { get; set; }
    }
}