using System.IO;
using DataLayer.Entities.Common;

namespace DataLayer.Entities
{
    public class DetectionImage : EntityBase, IImage
    {
        public int? DetectionId { get; set; }
        public Detection Detection { get; set; }
        public string Name { get; set; }
        public string Thumbnail { get; set; }
        public string Url { get; set; }

        public string GetPath()
        {
            return $"{nameof(DetectionImage)}/{DetectionId}";
        }
    }
}