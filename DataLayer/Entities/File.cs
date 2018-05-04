namespace DataLayer.Entities
{
    public class File : EntityBase
    {
        public string Name { get; set; }
        public string Url { get; set; }
        public int? FileSourceId { get; set; }
        public virtual File FileSource { get; set; }
    }
}