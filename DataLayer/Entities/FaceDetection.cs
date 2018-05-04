namespace DataLayer.Entities
{
    public class FaceDetection : EntityBase
    {
        public string Name { get; set; }
        public int DnnFaces { get; set; }
        public int HaarFaces { get; set; }
        public int? StatusId { get; set; }
        public Status Status { get; set; }
        public int? FileId { get; set; }
        public Status File { get; set; }
    }
}