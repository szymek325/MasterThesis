using System;
using System.IO;
using System.Text;
using System.Threading.Tasks;
using Dropbox.Api;
using Dropbox.Api.Files;
using DropboxIntegration.Client;
using Microsoft.Extensions.Logging;

namespace DropboxIntegration.Files
{
    public class FilesManager : IFilesManager
    {
        private readonly DropboxClient dbxClient;
        private readonly ILogger<FilesManager> logger;

        public FilesManager(ILogger<FilesManager> logger)
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

        public async Task<string> Download(string folder, string file)
        {
            var response = await dbxClient.Files.DownloadAsync(folder + "/" + file);
            var fileMetadata = await response.GetContentAsStringAsync();
            return fileMetadata;
        }
    }
}