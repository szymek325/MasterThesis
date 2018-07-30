namespace Domain.FaceDetection.DTO
{
    public class FaceRectangle
    {
        public int StartX { get; set; }
        public int EndX { get; set; }
        public int StartY { get; set; }
        public int EndY { get; set; }
        public int Area { get; set; }
    }
}