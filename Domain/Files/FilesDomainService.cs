using System.Collections.Generic;
using System.Threading.Tasks;
using AutoMapper;
using Domain.Files.DTO;
using DropboxIntegration.Files;
using Microsoft.EntityFrameworkCore.Storage;
using Microsoft.Extensions.Logging;

namespace Domain.Files
{
    public class FilesDomainService : IFilesDomainService
    {
        private readonly IFilesManager filesManager;
        private readonly ILogger<FilesDomainService> logger;
        private readonly IMapper mapper;

        public FilesDomainService(IFilesManager filesManager, ILogger<FilesDomainService> logger, IMapper mapper)
        {
            this.filesManager = filesManager;
            this.logger = logger;
            this.mapper = mapper;
        }

        public async Task Upload(IEnumerable<FileToUpload> files)
        {
            foreach (var fileToUpload in files)
                await filesManager.Upload("/reco", fileToUpload.FileName, fileToUpload.FileStream);
        }

        public async Task<FileToUpload> Download(string path,string fileName)
        {

            var file = await filesManager.DownloadAsStream(path, fileName);
            var fileToDownload = new FileToUpload
            {
                FileStream = file,
                FileName = fileName
            };
            return fileToDownload;
        }

    }
}