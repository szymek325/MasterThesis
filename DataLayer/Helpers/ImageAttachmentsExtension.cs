using System;
using DataLayer.Entities;

namespace DataLayer.Helpers
{
    public static class ImageAttachmentsExtension
    {
        public static string GetPath(this ImageAttachment image)
        {
            if (image.ImageAttachmentTypeId == ImageTypes.Detection)
                return $"{nameof(ImageTypes.Detection)}/{image.DetectionId}";
            if (image.ImageAttachmentTypeId == ImageTypes.DetectionResult)
                return $"{nameof(ImageTypes.DetectionResult)}/{image.DetectionResultId}";
            if (image.ImageAttachmentTypeId == ImageTypes.Person)
                return $"{nameof(ImageTypes.Person)}/{image.PersonId}";
            if (image.ImageAttachmentTypeId == ImageTypes.Recognition)
                return $"{nameof(ImageTypes.Recognition)}/{image.RecognitionId}";
            throw new ArgumentException("Wrong value of ImageAttachmentTypeId");
        }
    }
}