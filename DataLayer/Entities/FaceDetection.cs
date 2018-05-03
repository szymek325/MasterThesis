namespace DataLayer.Entities
{
    public class FaceDetection : EntityBase
    {
        public string Name { get; set; }
        public int DnnFaces { get; set; }
        public int HaarFaces { get; set; }
        public int? StatusId { get; set; }
        public virtual Status Status { get; set; }
    }
}