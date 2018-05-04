namespace DataLayer.Entities
{
    public class File : EntityBase
    {
        public string Name { get; set; }
        public string Path { get; set; }
        public string Batch { get; set; }
        public string Url { get; set; }
        public int? FileSourceId { get; set; }
        public File FileSource { get; set; }
        public int? PersonId { get; set; }
        public Person Person { get; set; }
    }
}