namespace DataLayer.Entities
{
    public class DetectionResult
    {
        public int StartX { get; set; }
        public int EndX { get; set; }
        public int StartY { get; set; }
        public int EndY { get; set; }
        public DetectionResultImage Image { get; set; }
        public int DetectionTypeId { get; set; }
        public DetectionType DetectionType { get; set; }
    }
}