using Microsoft.AspNetCore.Http;

namespace WebRazor.Models.Detection
{
    public class NewDetection
    {
        public string Name { get; set; }
        [ValidateFile(ErrorMessage = "Please select a PNG image smaller than 1MB")]
        public IFormFile File { get; set; }
    }
}