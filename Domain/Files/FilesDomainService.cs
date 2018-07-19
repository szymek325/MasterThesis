using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using DataLayer.Entities;
using DataLayer.Entities.Common;
using DataLayer.Repositories.Interface;
using Domain.Files.DTO;
using Domain.Files.Helpers;
using Dropbox.Client.Files;
using Dropbox.Client.Folders;
using Dropbox.Client.Links;
using Microsoft.Extensions.Logging;

namespace Domain.Files
{
    public class FilesDomainService : IFilesDomainService
    {
        private readonly IImageRepository imageRepository;
        private readonly IFilesClient filesClient;
        private readonly IFoldersClient foldersClient;
        private readonly ILogger<FilesDomainService> logger;
        private readonly IMapper mapper;
        private readonly IUrlClient urlClient;

        public FilesDomainService(IFilesClient filesClient, IFoldersClient foldersClient,
            ILogger<FilesDomainService> logger, IMapper mapper, IUrlClient urlClient, IImageRepository imageRepository)
        {
            this.filesClient = filesClient;
            this.foldersClient = foldersClient;
            this.logger = logger;
            this.mapper = mapper;
            this.urlClient = urlClient;
            this.imageRepository = imageRepository;
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


        public async Task GetThumbnail(ImageAttachment file)
        {
            try
            {
                file.Thumbnail =
                    await filesClient.DownloadThumbnail($"{file.GetPath()}", file.Name);
                imageRepository.Update(file);
                imageRepository.Save();
            }
            catch (Exception ex)
            {
                logger.LogError("Exception when creating thumbnail", ex);
            }
        }

        public async Task DeleteSingleFile(ImageAttachment file)
        {
            try
            {
                await filesClient.Delete(file.GetPath(), file.Name);
                imageRepository.Delete(file.Id);
                imageRepository.Save();
            }
            catch (Exception ex)
            {
                logger.LogError(
                    $"Exception when deleting file /{file.GetPath()}/{file.Name}", ex);
            }
        }

        public async Task DeleteFiles(IEnumerable<ImageAttachment> files)
        {
            try
            {
                files = files.ToList();
                if (files.Any())
                {
                    var firstFile = files.First();
                    await foldersClient.DeleteFolder($"{firstFile.GetPath()}");
                    foreach (var file in files)
                        imageRepository.Delete(file.Id);
                    imageRepository.Save();
                }
            }
            catch (Exception ex)
            {
                logger.LogError("exception when deleteing folder", ex);
            }
        }

        private string TurnIntoSourceLink(string url)
        {
            return url.Replace("www.dropbox.com", "dl.dropboxusercontent.com");
        }
    }
}