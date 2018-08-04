using System.Collections.Generic;
using Microsoft.AspNetCore.Http;

namespace WebRazor.Models.People
{
    public class NewPersonViewModel
    {
        public string Name { get; set; }

        [ValidateFile(ErrorMessage = "Please select a PNG/JPG/JPEG Image")]
        public List<IFormFile> Files { get; set; }
    }
}