using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using DataLayer.Entities;
using DataLayer.Repositories.Interface;
using Domain.Files.DTO;
using Dropbox.Client.Files;
using Dropbox.Client.Folders;
using Dropbox.Client.Links;
using Microsoft.Extensions.Logging;

namespace Domain.Files
{
    public class FilesDomainService : IFilesDomainService
    {
        private readonly IFilesClient filesClient;
        private readonly IDetectionImageRepository detectionImagesRepository;
        private readonly IFoldersClient foldersClient;
        private readonly ILogger<FilesDomainService> logger;
        private readonly IMapper mapper;
        private readonly IUrlClient urlClient;

        public FilesDomainService(IFilesClient filesClient, IFoldersClient foldersClient,
            ILogger<FilesDomainService> logger, IMapper mapper, IUrlClient urlClient, IDetectionImageRepository detectionImagesRepository)
        {
            this.filesClient = filesClient;
            this.foldersClient = foldersClient;
            this.logger = logger;
            this.mapper = mapper;
            this.urlClient = urlClient;
            this.detectionImagesRepository = detectionImagesRepository;
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


        public async Task GetThumbnail(File file)
        {
            try
            {
                file.Thumbnail =
                    await filesClient.DownloadThumbnail($"/{file.ParentGuid}", file.Name);
                detectionImagesRepository.Update(file);
                detectionImagesRepository.Save();
            }
            catch (Exception ex)
            {
                logger.LogError("Exception when creating thumbnail", ex);
            }
        }

        public async Task DeleteSingleFile(File file)
        {
            try
            {
                await filesClient.Delete($"/{file.ParentGuid}/{file.Name}");
                detectionImagesRepository.Delete(file.Id);
                detectionImagesRepository.Save();
            }
            catch (Exception ex)
            {
                logger.LogError(
                    $"Exception when deleting file /{file.ParentGuid}/{file.Name}", ex);
            }
        }

        public async Task DeleteFiles(IEnumerable<File> files)
        {
            files = files.ToList();
            if (files.Any())
            {
                await filesClient.Delete($"/{files.First().ParentGuid}");
                foreach (var file in files) detectionImagesRepository.Delete(file.Id);
                detectionImagesRepository.Save();
            }
        }

        private string TurnIntoSourceLink(string url)
        {
            return url.Replace("www.dropbox.com", "dl.dropboxusercontent.com");
        }
    }
}