using System.Threading.Tasks;
using Common;
using Dropbox.Api;
using Dropbox.Api.Files;
using DropboxIntegration.Configuration;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;

namespace DropboxIntegration.Folders
{
    public class FoldersClient : IFoldersClient
    {
        private readonly string basePath;
        private readonly DropboxClient dbxClient;
        private readonly IOptions<DropboxConfiguration> dropboxConfiguration;
        private readonly ILogger<FoldersClient> logger;

        public FoldersClient(ILogger<FoldersClient> logger, IOptions<DropboxConfiguration> dropboxConfiguration)
        {
            dbxClient = DropboxClientFactory.GetDropboxClient();
            this.logger = logger;
            this.dropboxConfiguration = dropboxConfiguration;
            basePath = dropboxConfiguration.Value.Path;
        }

        public async Task<ListFolderResult> GetFolderContent(string folderPath)
        {
            var path = $"{basePath}/{folderPath}";
            var response = await dbxClient.Files.ListFolderAsync(path);
            return response;
        }
    }
}