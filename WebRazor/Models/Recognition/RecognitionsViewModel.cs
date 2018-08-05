using System.Collections.Generic;
using Domain.FaceRecognition.DTO;

namespace WebRazor.Models.Recognition
{
    public class RecognitionsViewModel
    {
        public IEnumerable<RecognitionRequest> RecognitionRequests { get; set; }
    }
}