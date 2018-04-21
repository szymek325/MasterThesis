using System.IO;
using System.Threading.Tasks;
using Domain.Files;
using Domain.Providers;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.DataAnnotations.Internal;
using Microsoft.Extensions.Logging;

namespace Web.Controllers
{
    public class AttachmentsController : Controller
    {
        private readonly IFaceRecognitionJobProvider faceProvider;
        private readonly ILogger<AttachmentsController> logger;
        private readonly IFilesDomainService filesService;

        public AttachmentsController(IFaceRecognitionJobProvider faceProvider, ILogger<AttachmentsController> logger, IFilesDomainService filesService)
        {
            this.faceProvider = faceProvider;
            this.logger = logger;
            this.filesService = filesService;
        }

        [HttpPost("/upload")]
        public async Task<IActionResult> UploadAsync(IFormCollection collections)
        {
            var f = collections.Files;
            foreach (var file in f)
            {
                
                await filesService.Upload(file.OpenReadStream());
                //if (file.Length > 0)
                //{
                //    var filePath = Path.Combine(file.FileName);
                //    using (var fileStream = new FileStream(filePath, FileMode.Create))
                //    {
                //        filesService.Upload(fileStream);
                //    }
                //}
            }

            

            logger.LogDebug($"{f.Count} files received");
            var cos = faceProvider.GetAll();
            // process uploaded files
            // Don't rely on or trust the FileName property without validation.

            return Ok(new {count = f.Count});
        }
    }
}