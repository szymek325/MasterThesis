using DataLayer.Entities.Common;

namespace DataLayer.Entities
{
    public class DetectionResultImage : EntityBase, IImage
    {
        public int DetectionId { get; set; }
        public int DetectionResultId { get; set; }
        public DetectionResult DetectionResult { get; set; }
        public string Name { get; set; }
        public string Thumbnail { get; set; }
        public string Url { get; set; }

        public string GetPath()
        {
            return $"{nameof(DetectionResultImage)}/{DetectionId}";
        }
    }
}