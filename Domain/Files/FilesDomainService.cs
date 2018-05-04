using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using Domain.Files.DTO;
using Dropbox.Api;
using DropboxIntegration.Files;
using DropboxIntegration.Folders;
using DropboxIntegration.Links;
using Microsoft.Extensions.Logging;

namespace Domain.Files
{
    public class FilesDomainService : IFilesDomainService
    {
        private readonly IFilesClient filesClient;
        private readonly IFoldersClient foldersClient;
        private readonly ILogger<FilesDomainService> logger;
        private readonly IMapper mapper;
        private readonly IUrlClient urlClient;

        public FilesDomainService(IFilesClient filesClient, IFoldersClient foldersClient, IUrlClient urlClient,
            ILogger<FilesDomainService> logger, IMapper mapper)
        {
            this.filesClient = filesClient;
            this.foldersClient = foldersClient;
            this.urlClient = urlClient;
            this.logger = logger;
            this.mapper = mapper;
        }

        public async Task Upload(IEnumerable<FileToUpload> files, string location)
        {
            foreach (var fileToUpload in files)
                await filesClient.Upload(location, fileToUpload.FileName, fileToUpload.FileStream);
        }

        public async Task<IEnumerable<FileLink>> GetLinksToFilesInFolder(string folderPath)
        {
            var links = new List<FileLink>();

            try
            {
                var files = await foldersClient.GetFolderContent(folderPath);

                var retrievedLinks = await urlClient.GetAllLinks();
                var sharedLinkMetadatas = retrievedLinks.ToList();
                foreach (var file in files.Entries)
                {
                    var link = new FileLink
                    {
                        FileName = file.Name
                    };
                    var existingSharedFile = sharedLinkMetadatas.FirstOrDefault(x => x.PathLower == file.PathLower);
                    link.Url = TurnIntoSourceLink(
                        existingSharedFile != null
                            ? existingSharedFile.Url
                            : await urlClient.CreateLinkToFile(file.PathLower));
                    links.Add(link);
                }
            }
            catch (Exception ex)
            {
            }

            return links;
        }

        private string TurnIntoSourceLink(string url)
        {
            return url.Replace("www.dropbox.com", "dl.dropboxusercontent.com");
        }
    }
}