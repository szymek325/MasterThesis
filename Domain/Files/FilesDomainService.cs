using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using Domain.Files.DTO;
using Dropbox.Api;
using Dropbox.Api.Files;
using DropboxIntegration.Files;
using DropboxIntegration.Folders;
using DropboxIntegration.Links;
using Microsoft.Extensions.Logging;

namespace Domain.Files
{
    public class FilesDomainService : IFilesDomainService
    {
        private readonly IFilesManager filesManager;
        private readonly IFoldersManager foldersManager;
        private readonly IUrlManager urlManager;
        private readonly ILogger<FilesDomainService> logger;
        private readonly IMapper mapper;

        public FilesDomainService(IFilesManager filesManager, IFoldersManager foldersManager, IUrlManager urlManager,
            ILogger<FilesDomainService> logger, IMapper mapper)
        {
            this.filesManager = filesManager;
            this.foldersManager = foldersManager;
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

        public async Task<string> GetLinkToFile(string path, string fileName)
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
                link = await GetExistingLink(fileName);
            }
            link = TurnIntoSourceLink(link);
            return link;
        }

        public async Task<IEnumerable<FileLink>> GetLinksToFilesInFolder(string folderPath)
        {
            var links = new List<FileLink>();

            try
            {
                var files = await foldersManager.GetFolderContent(folderPath);

                var retrievedLinks = await urlManager.GetAllLinksFromFolder(folderPath);
                var sharedLinkMetadatas = retrievedLinks.ToList();
                foreach (var file in files.Entries)
                {
                    var link = new FileLink
                    {
                        FileName = file.Name
                    };
                    var existingSharedFile = sharedLinkMetadatas.FirstOrDefault(x => x.Name == file.Name);
                    link.Url = existingSharedFile != null
                        ? existingSharedFile.Url
                        : TurnIntoSourceLink(await urlManager.CreateLinkToFile(file.PathLower));
                    links.Add(link);
                }
            }
            catch(Exception ex)
            {

            }

            return links;
        }



        private async Task<string> GetExistingLink(string fileName)
        {
            var links = await urlManager.GetAllLinks();
            var link = links.FirstOrDefault(x => x.Name == fileName)?.Url;
            return link;
        }

        private string TurnIntoSourceLink(string url)
        {
            return url.Replace("www.dropbox.com", "dl.dropboxusercontent.com");
        }
    }
}