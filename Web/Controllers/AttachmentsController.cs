using System.Threading.Tasks;
using Domain.Providers;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Web.Controllers
{
    public class AttachmentsController : Controller
    {
        private readonly IFaceRecognitionJobProvider faceProvider;

        public AttachmentsController(IFaceRecognitionJobProvider faceProvider)
        {
            this.faceProvider = faceProvider;
        }

        [HttpPost("/upload")]
        public async Task<IActionResult> UploadAsync(IFormCollection collections)
        {
            var f = collections.Files;

            foreach (var file in f)
            {
                //....
            }

            var cos=faceProvider.GetAll();
            // process uploaded files
            // Don't rely on or trust the FileName property without validation.

            return Ok(new {count = f.Count});
        }
    }
}