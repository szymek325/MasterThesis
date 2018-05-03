namespace DataLayer.Entities
{
    public class FaceRecognitionJob : EntityBase
    {
        public string Name { get; set; }
        public int Type { get; set; }
        public string Description { get; set; }
    }
}