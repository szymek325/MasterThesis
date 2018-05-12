namespace DataLayer.Entities
{
    public class File : EntityBase
    {
        public string Name { get; set; }
        public string Thumbnail { get; set; }
        public string Url { get; set; }
        public string ParentGuid { get; set; }
        public int? PersonId { get; set; }
        public Person Person { get; set; }
        public int? FaceDetectionId { get; set; }
        public FaceDetection FaceDetection { get; set; }
        public int? FaceRecognitionId { get; set; }
        public FaceRecognition FaceRecognition { get; set; }
    }
}