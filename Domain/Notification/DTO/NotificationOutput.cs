using Domain.Files.DTO;

namespace Domain.Notification.DTO
{
    public class NotificationOutput
    {
        public int Id { get; set; }
        public string Message { get; set; }
        public string Thumbnail { get; set; }
        public FileLink FileLink { get; set; }
        public int Type { get; set; }
    }
}