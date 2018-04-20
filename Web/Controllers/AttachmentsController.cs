using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Web.Controllers
{
    public class AttachmentsController : Controller
    {
        [HttpPost("/upload")]
        public async Task<IActionResult> UploadAsync(IFormCollection collections)
        {
            var f = collections.Files;
            
            foreach (var file in f)
            {
                //....
            }

            // process uploaded files
            // Don't rely on or trust the FileName property without validation.

            return Ok(new { count = f.Count});

        }

    }
}
