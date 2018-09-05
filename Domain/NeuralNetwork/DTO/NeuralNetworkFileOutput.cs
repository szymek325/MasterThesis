namespace Domain.NeuralNetwork.DTO
{
    public class NeuralNetworkFileOutput
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string TypeName { get; set; }
        public string ProcessingTime { get; set; }
        public string TrainingTime { get; set; }
    }
}