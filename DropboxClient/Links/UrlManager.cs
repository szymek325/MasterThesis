using System.Collections.Generic;
using System.Threading.Tasks;
using Dropbox.Api;
using Dropbox.Api.Sharing;
using DropboxIntegration.Configuration;
using Microsoft.Extensions.Logging;

namespace DropboxIntegration.Links
{
    public class UrlManager : IUrlManager
    {
        private readonly DropboxClient dbxClient;
        private readonly ILogger<UrlManager> logger;

        public UrlManager( ILogger<UrlManager> logger)
        {
            dbxClient = DropboxClientFactory.GetDropboxClient();
            this.logger = logger;
        }

        public async Task<string> CreateLinkToFile(string pathToFile)
        {
            var response = await dbxClient.Sharing.CreateSharedLinkWithSettingsAsync(pathToFile);
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