namespace DataLayer.Entities
{
    public class DetectionImage : EntityBase
    {
        public string Name { get; set; }
        public string Thumbnail { get; set; }
        public string Url { get; set; }
        public int? DetectionId { get; set; }
        public Detection Detection { get; set; }
    }
}