using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Net.Mime;
using Microsoft.AspNetCore.Http;

public class ValidateFileAttribute : RequiredAttribute
{
    public override bool IsValid(object value)
    {
        var acceptedFilesTypes = new List<string>
        {
            "image/jpg",
            "image/png",
            "image/jpeg"
        };
        var file = value as IFormFile;
        if (file == null)
        {
            return false;
        }

        try
        {
            if (acceptedFilesTypes.Contains(file.ContentType))
            {
                return true;
            }
        }
        catch
        {

        }
        return false;
    }
}