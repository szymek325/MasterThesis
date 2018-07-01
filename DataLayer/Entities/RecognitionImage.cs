using System.IO;
using DataLayer.Entities.Common;

namespace DataLayer.Entities
{
    public class RecognitionImage : EntityBase, IImage
    {
        public int? RecognitionId { get; set; }
        public Recognition Recognition { get; set; }
        public string Name { get; set; }
        public string Thumbnail { get; set; }
        public string Url { get; set; }
        public string GetPath()
        {
            return $"{nameof(RecognitionImage)}/{Id}";
        }
    }
}