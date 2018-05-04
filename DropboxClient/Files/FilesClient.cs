using System;
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
            var a = "";
            var argsss = new List<ThumbnailArg>
            {
                new ThumbnailArg(folder + "/" + file)
            };
            var args = new GetThumbnailBatchArg(argsss);
            try
            {
                var response = await dbxClient.Files.GetThumbnailBatchAsync(args);
                var re = response.Entries[0].AsSuccess;
                var c = re.Value;
                a = c.Thumbnail;
            }
            catch (Exception ex)
            {
            }

            return a;
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