using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using System.Threading.Tasks;
using DropboxIntegration.Files;
using Microsoft.Extensions.Logging;

namespace Domain.Files
{
    public class FilesDomainService:IFilesDomainService
    {
        private readonly IFilesManager filesManager;
        private readonly ILogger<FilesDomainService> logger;

        public FilesDomainService(IFilesManager filesManager, ILogger<FilesDomainService> logger)
        {
            this.filesManager = filesManager;
            this.logger = logger;
        }

        public async Task Upload(Stream formFileCollection)
        {
            await filesManager.Upload("/reco", "file1", formFileCollection);
        }
    }
}
