using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using AutoMapper.XpressionMapper.Extensions;
using Domain.Files;
using Domain.Providers;
using DropboxIntegration.Files.DTO;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace Web.Controllers
{
    public class AttachmentsController : Controller
    {
        private readonly IFaceRecognitionJobProvider faceProvider;
        private readonly IFilesDomainService filesService;
        private readonly ILogger<AttachmentsController> logger;
        private readonly IMapper mapper;

        public AttachmentsController(IFaceRecognitionJobProvider faceProvider, ILogger<AttachmentsController> logger,
            IFilesDomainService filesService, IMapper mapper)
        {
            this.faceProvider = faceProvider;
            this.logger = logger;
            this.filesService = filesService;
            this.mapper = mapper;
        }

        [HttpPost("/upload")]
        public async Task<IActionResult> UploadAsync(IFormCollection collections)
        {
            var f = collections.Files;
            //var files = mapper.Map<IEnumerable<IFormFile>,IEnumerable<FileToUpload>>(f);
            var files = mapper.Map<IEnumerable<FileToUpload>>(f);

            var c = f.Select(x =>
                new FileToUpload
                {
                    FileName = x.FileName,
                    FileStream = x.OpenReadStream()
                }
            );
            foreach (var file in f)
                await filesService.Upload(file.OpenReadStream());
            //if (file.Length > 0)
            //{
            //    var filePath = Path.Combine(file.FileName);
            //    using (var fileStream = new FileStream(filePath, FileMode.Create))
            //    {
            //        filesService.Upload(fileStream);
            //    }
            //}


            logger.LogDebug($"{f.Count} files received");
            var cos = faceProvider.GetAll();
            // process uploaded files
            // Don't rely on or trust the FileName property without validation.

            return Ok(new {count = f.Count});
        }
    }
}