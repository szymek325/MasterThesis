namespace DataLayer.Entities
{
    public class PersonImage
    {
        public string Name { get; set; }
        public string Thumbnail { get; set; }
        public string Url { get; set; }
        public int? PersonId { get; set; }
        public Person Person { get; set; }
    }
}