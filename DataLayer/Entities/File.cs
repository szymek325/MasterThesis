using System.ComponentModel.DataAnnotations.Schema;

namespace DataLayer.Entities
{
    public class File : EntityBase
    {
        public string Name { get; set; }
        public string Thumbnail { get; set; }
        public string Url { get; set; }
        public string PersonGuid { get; set; }
        public Person Person { get; set; }
        public string FaceDetectionGuid { get; set; }
        public FaceDetection FaceDetection { get; set; }
    }
}