using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
using Domain.People.DTO;
using Microsoft.AspNetCore.Http;

namespace WebRazor.Validators
{
    public class ValidatePeople : RequiredAttribute
    {
        public override bool IsValid(object value)
        {
            var acceptedFilesTypes = new List<string>
            {
                "image/jpg",
                "image/png",
                "image/jpeg"
            };
            var files = value as IEnumerable<PersonAsCheckbox>;
            if (files == null)
                return false;
            var formFiles = files.ToList();

            var pickedPeople = formFiles.Where(x => x.IsChecked);

            return pickedPeople.Count() >= 2;
        }
    }
}
