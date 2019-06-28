using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Http;
using WebRazor.Validators;

namespace WebRazor.Models.People
{
    public class NewPersonViewModel
    {
        [Required] public string Name { get; set; }

        [Required]
        [ValidateFiles(ErrorMessage = "Please select at least two PNG/JPG/JPEG Images")]
        public List<IFormFile> Files { get; set; }
    }
}