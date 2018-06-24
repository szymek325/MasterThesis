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
        private readonly IDropboxClientFactory dropboxClientFactory;
        private readonly ILogger<FoldersClient> logger;


        public FoldersClient(ILogger<FoldersClient> logger, IDropboxClientFactory dropboxClientFactory)
        {
            this.logger = logger;
            this.dropboxClientFactory = dropboxClientFactory;
            basePath = this.dropboxClientFactory.GetBasePath();
            dbxClient = this.dropboxClientFactory.GetDropboxClient();
        }

        public async Task<ListFolderResult> GetFolderContent(string folderPath)
        {
            var path = $"{basePath}/{folderPath}";
            var response = await dbxClient.Files.ListFolderAsync(path);
            return response;
        }
    }
}