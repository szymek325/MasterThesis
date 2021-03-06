﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Mime;
using System.Threading.Tasks;
using AutoMapper;
using DataLayer.Entities;
using DataLayer.Entities.Common;
using DataLayer.Helpers;
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
                logger.LogError(ex, "exception when retrieving links");
            }

            return links;
        }

        public async Task GetLinkToFile(ImageAttachment image)
        {
            //TODO Message = "There is already an open DataReader associated with this Command which must be closed first."
            //TODO need to aad getting links from exisiting list if exception occurs
            //TODO add MultipleActiveResultSets=true and think further
            var imagePath = $"{image.GetPath()}/{image.Name}";
            try
            {
                var url=await urlClient.CreateLinkToFileWithMissingBasePath(imagePath);
                image.Url = TurnIntoSourceLink(url);
                imageRepository.Update(image);
                imageRepository.Save();
            }
            catch (Exception ex)
            {
                logger.LogError(ex,$"Exception when generating link for file: {imagePath}");
            }
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
                logger.LogError(ex,"Exception when creating thumbnail");
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
                logger.LogError(ex, $"Exception when deleting file /{file.GetPath()}/{file.Name}");
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
                logger.LogError(ex, "exception when deleteing folder");
            }
        }

        private string TurnIntoSourceLink(string url)
        {
            return url.Replace("www.dropbox.com", "dl.dropboxusercontent.com");
        }
    }
}