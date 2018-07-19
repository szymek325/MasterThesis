using System;
using DataLayer.Entities;

namespace Domain.Files.Helpers
{
    public static class ImageAttachmentsExtension
    {
        public static string GetPath(this ImageAttachment image)
        {
            if (image.ImageAttachmentTypeId == ImageTypes.DetectionImage)
                return $"{nameof(ImageTypes.DetectionImage)}/{image.DetectionId}";
            if (image.ImageAttachmentTypeId == ImageTypes.DetectionResultImage)
                return $"{nameof(ImageTypes.DetectionResultImage)}/{image.DetectionResultId}";
            if (image.ImageAttachmentTypeId == ImageTypes.PersonImage)
                return $"{nameof(ImageTypes.PersonImage)}/{image.PersonId}";
            if (image.ImageAttachmentTypeId == ImageTypes.RecognitionImage)
                return $"{nameof(ImageTypes.RecognitionImage)}/{image.RecognitionId}";
            throw new ArgumentException("Wrong value of ImageAttachmentTypeId");
        }
    }
}