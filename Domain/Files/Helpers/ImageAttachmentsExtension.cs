using System;
using DataLayer.Entities;

namespace Domain.Files.Helpers
{
    public static class ImageAttachmentsExtension
    {
        public static string GetPath(this ImageAttachment image)
        {
            if (image.ImageAttachmentTypeId == ImagesTypesEnum.DetectionImage)
                return $"{nameof(ImagesTypesEnum.DetectionImage)}/{image.DetectionId}";
            if (image.ImageAttachmentTypeId == ImagesTypesEnum.DetectionImageResult)
                return $"{nameof(ImagesTypesEnum.DetectionImageResult)}/{image.DetectionResultId}";
            if (image.ImageAttachmentTypeId == ImagesTypesEnum.PersonImage)
                return $"{nameof(ImagesTypesEnum.PersonImage)}/{image.PersonId}";
            if (image.ImageAttachmentTypeId == ImagesTypesEnum.RecognitionImage)
                return $"{nameof(ImagesTypesEnum.RecognitionImage)}/{image.RecognitionId}";
            throw new ArgumentException("Wrong value of ImageAttachmentTypeId");
        }
    }
}