using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using Domain.Files;
using Domain.Files.DTO;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace Web.Controllers
{
    public class AttachmentsController : Controller
    {
        private readonly IFilesDomainService filesService;
        private readonly ILogger<AttachmentsController> logger;
        private readonly IMapper mapper;

        public AttachmentsController(IFilesDomainService filesService, ILogger<AttachmentsController> logger,
            IMapper mapper)
        {
            this.filesService = filesService;
            this.logger = logger;
            this.mapper = mapper;
        }

        [HttpPost("/upload")]
        public async Task<IActionResult> UploadAsync(IFormCollection collections)
        {
            var files = mapper.Map<IEnumerable<FileToUpload>>(collections.Files);
            await filesService.Upload(files);


            return Ok(new {count = files.Count()});
        }

        //[HttpGet("/getFile")]
        //public async Task<IActionResult> GetFile(IFormCollection collections)
        //{
        //    var files = mapper.Map<IEnumerable<FileToUpload>>(collections.Files);
        //    await filesService.Upload(files);


        //    return Ok(new { count = files.Count() });
        }
    }
}