using System.Threading.Tasks;
using Dropbox.Api;
using Microsoft.Extensions.Logging;

namespace DropboxIntegration.Links
{
    public class FileUrlProvider : IFileUrlProvider
    {
        private readonly DropboxClient dbxClient;
        private readonly ILogger<FileUrlProvider> logger;

        public FileUrlProvider(DropboxClient dbxClient, ILogger<FileUrlProvider> logger)
        {
            this.dbxClient = dbxClient;
            this.logger = logger;
        }

        public async Task<string> GetLink(string pathToFile)
        {
            var response = await dbxClient.Sharing.CreateSharedLinkWithSettingsAsync(pathToFile);
            return response.Url;
        }
    }
}