using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using DataLayer.Entities;
using DataLayer.Repositories.Interface;
using Domain.Files.DTO;
using DropboxIntegration.Files;
using DropboxIntegration.Folders;
using DropboxIntegration.Links;
using Microsoft.Extensions.Logging;

namespace Domain.Files
{
    public class FilesDomainService : IFilesDomainService
    {
        private readonly IFilesClient filesClient;
        private readonly IFileRepository filesRepository;
        private readonly IFoldersClient foldersClient;
        private readonly ILogger<FilesDomainService> logger;
        private readonly IMapper mapper;
        private readonly IUrlClient urlClient;

        public FilesDomainService(IFilesClient filesClient, IFoldersClient foldersClient,
            ILogger<FilesDomainService> logger, IMapper mapper, IUrlClient urlClient, IFileRepository filesRepository)
        {
            this.filesClient = filesClient;
            this.foldersClient = foldersClient;
            this.logger = logger;
            this.mapper = mapper;
            this.urlClient = urlClient;
            this.filesRepository = filesRepository;
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
                logger.LogError("exception when retrieving links", ex);
            }

            return links;
        }

        public async Task Delete(IEnumerable<File> files)
        {
            try
            {
                foreach (var filetoDelete in files)
                {
                    await filesClient.Delete(filetoDelete.Path + "/" + filetoDelete.Name);
                    filesRepository.Delete(filetoDelete.Id);
                }

                filesRepository.Save();
            }
            catch (Exception ex)
            {
                logger.LogError("Exception when deleting files.", ex);
            }
        }

        public async Task GetThumbnail(File file)
        {
            try
            {
                file.Thumbnail = await filesClient.DownloadThumbnail(file.Path,file.Name);
                filesRepository.Update(file);
                filesRepository.Save();
            }
            catch(Exception ex)
            {
                logger.LogError("Exception when creating thumbnail",ex);
            }
        }

        private string TurnIntoSourceLink(string url)
        {
            return url.Replace("www.dropbox.com", "dl.dropboxusercontent.com");
        }
    }
}