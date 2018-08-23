using DataLayer.Entities.Common;

namespace DataLayer.Entities
{
    public class ImageAttachment : EntityBase
    {
        public string Name { get; set; }
        public string Thumbnail { get; set; }
        public string Url { get; set; }
        public int ImageAttachmentTypeId { get; set; }
        public int? DetectionId { get; set; }
        public int? DetectionResultId { get; set; }
        public int? RecognitionId { get; set; }
        public int? PersonId { get; set; }
        public int? NotificationId { get; set; }
        public virtual ImageAttachmentType ImageAttachmentType { get; set; }
        public virtual Detection Detection { get; set; }
        public virtual DetectionResult DetectionResult { get; set; }
        public virtual Recognition Recognition { get; set; }
        public virtual Person Person { get; set; }
        public virtual Notification Notification { get; set; }
    }
}