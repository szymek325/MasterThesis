﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using DataLayer.Entities.Common;
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
        private readonly IFileRepository fileRepository;
        private readonly IFilesClient filesClient;
        private readonly IFoldersClient foldersClient;
        private readonly ILogger<FilesDomainService> logger;
        private readonly IMapper mapper;
        private readonly IUrlClient urlClient;

        public FilesDomainService(IFilesClient filesClient, IFoldersClient foldersClient,
            ILogger<FilesDomainService> logger, IMapper mapper, IUrlClient urlClient, IFileRepository fileRepository)
        {
            this.filesClient = filesClient;
            this.foldersClient = foldersClient;
            this.logger = logger;
            this.mapper = mapper;
            this.urlClient = urlClient;
            this.fileRepository = fileRepository;
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


        public async Task GetThumbnail(IImage file)
        {
            try
            {
                file.Thumbnail =
                    await filesClient.DownloadThumbnail($"{file.GetPath()}", file.Name);
                fileRepository.Update(file);
                fileRepository.Save();
            }
            catch (Exception ex)
            {
                logger.LogError("Exception when creating thumbnail", ex);
            }
        }

        public async Task DeleteSingleFile(IImage file)
        {
            try
            {
                await filesClient.Delete(file.GetPath(), file.Name);
                fileRepository.Delete(file.Id);
                fileRepository.Save();
            }
            catch (Exception ex)
            {
                logger.LogError(
                    $"Exception when deleting file /{file.GetPath()}/{file.Name}", ex);
            }
        }

        public async Task DeleteFiles(IEnumerable<IImage> files)
        {
            try
            {
                files = files.ToList();
                if (files.Any())
                {
                    var firstFile = files.First();
                    await foldersClient.DeleteFolder($"{firstFile.GetPath()}");
                    foreach (var file in files)
                        fileRepository.Delete(file.Id);
                    fileRepository.Save();
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