using Microsoft.AspNetCore.Http;

namespace WebRazor.Models.Detection
{
    public class NewDetectionViewModel
    {
        public string Name { get; set; }
        [ValidateFile(ErrorMessage = "Please select a PNG/JPG/JPEG Image")]
        public IFormFile File { get; set; }
    }
}