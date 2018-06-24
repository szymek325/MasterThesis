namespace DataLayer.Entities.Common
{
    public interface IImage
    {
        int Id { get; set; }
        string Name { get; set; }
        string Thumbnail { get; set; }
        string Url { get; set; }
    }
}