using System.Collections.Generic;
using System.Threading.Tasks;
using Dropbox.Api;
using Dropbox.Api.Sharing;
using Dropbox.Client.Configuration;
using Microsoft.Extensions.Logging;

namespace Dropbox.Client.Links
{
    public class UrlClient : IUrlClient
    {
        private readonly DropboxClient dbxClient;
        private readonly IDropboxClientFactory dropboxClientFactory;
        private readonly ILogger<UrlClient> logger;
        private readonly string basePath;

        public UrlClient( ILogger<UrlClient> logger, IDropboxClientFactory dropboxClientFactory)
        {
            this.logger = logger;
            this.dropboxClientFactory = dropboxClientFactory;
            dbxClient = this.dropboxClientFactory.GetDropboxClient();
            basePath = this.dropboxClientFactory.GetBasePath();
        }

        public async Task<string> CreateLinkToFile(string pathToFile)
        {
            var response = await dbxClient.Sharing.CreateSharedLinkWithSettingsAsync(pathToFile);
            return response.Url;
        }
        public async Task<string> CreateLinkToFileWithMissingBasePath(string pathToFile)
        {
            var path = $"{basePath}/{pathToFile}";
            var response = await dbxClient.Sharing.CreateSharedLinkWithSettingsAsync(path);
            return response.Url;
        }

        public async Task<IEnumerable<SharedLinkMetadata>> GetAllLinks()
        {
            var response = await dbxClient.Sharing.ListSharedLinksAsync();
            return response.Links;
        }

        public async Task<IEnumerable<SharedLinkMetadata>> GetAllLinksFromFolder(string pathToFolder)
        {
            var response = await dbxClient.Sharing.ListSharedLinksAsync(pathToFolder);
            return response.Links;
        }
    }
}