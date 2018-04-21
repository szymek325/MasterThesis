using System.Collections.Generic;
using System.Threading.Tasks;
using DropboxIntegration.Files;
using DropboxIntegration.Files.DTO;
using Microsoft.Extensions.Logging;

namespace Domain.Files
{
    public class FilesDomainService : IFilesDomainService
    {
        private readonly IFilesManager filesManager;
        private readonly ILogger<FilesDomainService> logger;

        public FilesDomainService(IFilesManager filesManager, ILogger<FilesDomainService> logger)
        {
            this.filesManager = filesManager;
            this.logger = logger;
        }

        public async Task Upload(IEnumerable<FileToUpload> files)
        {
            foreach (var fileToUpload in files)
                await filesManager.Upload("/reco", fileToUpload.FileName, fileToUpload.FileStream);
        }
    }
}