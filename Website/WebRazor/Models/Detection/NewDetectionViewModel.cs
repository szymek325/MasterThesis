using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Http;
using WebRazor.Validators;

namespace WebRazor.Models.Detection
{
    public class NewDetectionViewModel
    {
        [Required]
        public string Name { get; set; }
        [Required]
        [ValidateFile(ErrorMessage = "Please select a PNG/JPG/JPEG Image")]
        public IFormFile File { get; set; }
    }
}