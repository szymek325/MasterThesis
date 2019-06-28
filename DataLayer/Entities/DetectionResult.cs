using System.Collections.Generic;
using DataLayer.Entities.Common;

namespace DataLayer.Entities
{
    public class DetectionResult : EntityBase
    {
        public int DetectionId { get; set; }
        public int DetectionTypeId { get; set; }
        public virtual Detection Detection { get; set; }
        public virtual DetectionType DetectionType { get; set; }
        public virtual ImageAttachment Image { get; set; }
        public IEnumerable<DetectionRectangle> FaceRectangles { get; set; }
        public string ProcessingTime { get; set; }
    }
}