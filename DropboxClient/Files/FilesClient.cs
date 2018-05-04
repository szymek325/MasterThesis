﻿using System;
using System.Collections.Generic;
using System.IO;
using System.Threading.Tasks;
using Dropbox.Api;
using Dropbox.Api.Files;
using DropboxIntegration.Configuration;
using Microsoft.Extensions.Logging;

namespace DropboxIntegration.Files
{
    public class FilesClient : IFilesClient
    {
        private readonly DropboxClient dbxClient;
        private readonly ILogger<FilesClient> logger;

        public FilesClient(ILogger<FilesClient> logger)
        {
            this.logger = logger;
            dbxClient = DropboxClientFactory.GetDropboxClient();
        }

        public async Task Upload(string folder, string file, Stream content)
        {
            var updated = await dbxClient.Files.UploadAsync(
                folder + "/" + file,
                WriteMode.Overwrite.Instance,
                body: content);
            Console.WriteLine($"Saved {folder}/{file} rev {updated.Rev}");
        }

        public async Task<string> DownloadThumbnail(string folder, string file)
        {
            var thumbnail = "";
            var requestArguments = new GetThumbnailBatchArg(new List<ThumbnailArg>
            {
                new ThumbnailArg(folder + "/" + file)
            });
            try
            {
                var response = await dbxClient.Files.GetThumbnailBatchAsync(requestArguments);
                thumbnail = response.Entries[0].AsSuccess.Value.Thumbnail;
            }
            catch (Exception ex)
            {
                logger.LogError("Exception when retrieving thumbnail");
            }

            return thumbnail;
        }

        public async Task<string> DownloadAsString(string folder, string file)
        {
            var response = await dbxClient.Files.DownloadAsync(folder + "/" + file);
            var fileMetadata = await response.GetContentAsStringAsync();
            return fileMetadata;
        }

        public async Task<Stream> DownloadAsStream(string folder, string file)
        {
            var response = await dbxClient.Files.DownloadAsync(folder + "/" + file);
            var fileMetadata = await response.GetContentAsStreamAsync();
            return fileMetadata;
        }
    }
}