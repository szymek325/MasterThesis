using System.Threading.Tasks;
using Dropbox.Api;
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
    }
}