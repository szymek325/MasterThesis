using DataLayer.Entities.Common;

namespace DataLayer.Entities
{
    public class DetectionResult : EntityBase
    {
        public int StartX { get; set; }
        public int EndX { get; set; }
        public int StartY { get; set; }
        public int EndY { get; set; }
        public int DetectionId { get; set; }
        public int DetectionTypeId { get; set; }
        public virtual Detection Detection { get; set; }
        public virtual DetectionType DetectionType { get; set; }
        public virtual ImageAttachment Image { get; set; }
    }
}