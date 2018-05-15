using System;
using System.Collections.Generic;
using System.IO;
using System.Threading.Tasks;
using Common;
using Dropbox.Api;
using Dropbox.Api.Files;
using DropboxIntegration.Configuration;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;

namespace DropboxIntegration.Files
{
    public class FilesClient : IFilesClient
    {
        private readonly DropboxClient dbxClient;
        private readonly ILogger<FilesClient> logger;
        private readonly IOptions<DropboxConfiguration> dropboxConfiguration;
        private readonly string basePath;

        public FilesClient(ILogger<FilesClient> logger, IOptions<DropboxConfiguration> dropboxConfiguration)
        {
            dbxClient = DropboxClientFactory.GetDropboxClient();
            this.logger = logger;
            this.dropboxConfiguration = dropboxConfiguration;
            basePath = dropboxConfiguration.Value.Path;
        }

        public async Task Delete(string folder, string file=null)
        {
            var path = $"{basePath}/{folder}/{file}";
            var result = await dbxClient.Files.DeleteV2Async(path);
            if (result.Metadata.IsDeleted)
            {
                logger.LogError($"Files were not deleted from {path}");
                throw  new Exception("Files were not deleted.");
            }
        }

        public async Task Upload(string folder, string file, Stream content)
        {
            var path = $"{basePath}/{folder}/{file}";
            var updated = await dbxClient.Files.UploadAsync(
                path,
                WriteMode.Overwrite.Instance,
                body: content);
            logger.LogInformation($"Saved {path} rev {updated.Rev}");
        }

        public async Task<string> DownloadThumbnail(string folder, string file)
        {
            var thumbnail = "";
            var path = $"{basePath}/{folder}/{file}";
            var requestArguments = new GetThumbnailBatchArg(new List<ThumbnailArg>
            {
                new ThumbnailArg(path)
            });
            try
            {
                var response = await dbxClient.Files.GetThumbnailBatchAsync(requestArguments);
                thumbnail = response.Entries[0].AsSuccess.Value.Thumbnail;
            }
            catch (Exception ex)
            {
                logger.LogError($"Exception when retrieving thumbnail from path {path}");
            }

            return thumbnail;
        }
    }
}