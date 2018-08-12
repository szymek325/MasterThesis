using DataLayer.Entities.Common;

namespace DataLayer.Entities
{
    public class DetectionRectangle : EntityBase
    {
        public int StartX { get; set; }
        public int EndX { get; set; }
        public int StartY { get; set; }
        public int EndY { get; set; }
        public int Area { get; set; }
        public int  DetectionResultId { get; set; }
        public virtual DetectionResult DetectionResult { get; set; }
    }
}