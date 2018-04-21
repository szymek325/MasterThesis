using System.Threading.Tasks;
using Domain.Providers;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace Web.Controllers
{
    public class AttachmentsController : Controller
    {
        private readonly IFaceRecognitionJobProvider faceProvider;
        private readonly ILogger<AttachmentsController> logger;


        public AttachmentsController(IFaceRecognitionJobProvider faceProvider, ILogger<AttachmentsController> logger)
        {
            this.faceProvider = faceProvider;
            this.logger = logger;
        }

        [HttpPost("/upload")]
        public async Task<IActionResult> UploadAsync(IFormCollection collections)
        {
            var f = collections.Files;

            foreach (var file in f)
            {
                //....
            }

            logger.LogDebug($"{f.Count} files received");
            var cos = faceProvider.GetAll();
            // process uploaded files
            // Don't rely on or trust the FileName property without validation.

            return Ok(new {count = f.Count});
        }
    }
}