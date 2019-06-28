using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using Microsoft.AspNetCore.Http;

namespace WebRazor.Validators
{
    public class ValidateFilesAttribute : RequiredAttribute
    {
        public override bool IsValid(object value)
        {
            var acceptedFilesTypes = new List<string>
            {
                "image/jpg",
                "image/png",
                "image/jpeg"
            };
            var files = value as IEnumerable<IFormFile>;
            if (files == null)
                return false;
            var formFiles = files.ToList();
            if (formFiles.Count<2)
                return false;

            try
            {
                foreach (var file in formFiles)
                    if (!acceptedFilesTypes.Contains(file.ContentType))
                        return false;

                return true;
            }
            catch
            {
            }


            return false;
        }
    }
}