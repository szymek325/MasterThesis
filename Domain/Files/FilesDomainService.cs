using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using Domain.Files.DTO;
using Dropbox.Api;
using Dropbox.Api.Files;
using DropboxIntegration.Files;
using DropboxIntegration.Links;
using Microsoft.Extensions.Logging;

namespace Domain.Files
{
    public class FilesDomainService : IFilesDomainService
    {
        private readonly IFilesManager filesManager;
        private readonly IUrlManager urlManager;
        private readonly ILogger<FilesDomainService> logger;
        private readonly IMapper mapper;

        public FilesDomainService(IFilesManager filesManager, IUrlManager urlManager, ILogger<FilesDomainService> logger, IMapper mapper)
        {
            this.filesManager = filesManager;
            this.urlManager = urlManager;
            this.logger = logger;
            this.mapper = mapper;
        }

        public async Task Upload(IEnumerable<FileToUpload> files, string location="/reco")
        {
            foreach (var fileToUpload in files)
                await filesManager.Upload(location, fileToUpload.FileName, fileToUpload.FileStream);
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

        public async Task<FileLink> GetLinkToFile(string path, string fileName)
        {
            var link="";
            try
            {
                var pathToFile = $"{path}/{fileName}";
                link = await urlManager.CreateLinkToFile(pathToFile);
            }
            catch (DropboxException ex)
            {
                logger.LogError($"{ex.Message}");
                link = await GetExistingLink(fileName, link);
            }
            link = TurnIntoSourceLink(link);
            return new FileLink {Url = link};
        }

        public async Task<IEnumerable<FileLink>> GetLinks(string folderPath)
        {
            var links= new List<FileLink>();
            return links;
        }

        private async Task<string> GetExistingLink(string fileName, string link)
        {
            var cos = await urlManager.GetAllLinks();
            link = cos.FirstOrDefault(x => x.Name == fileName).Url;
            return link;
        }

        private string TurnIntoSourceLink(string url)
        {
            return url.Replace("www.dropbox.com", "dl.dropboxusercontent.com");
        }
    }
}