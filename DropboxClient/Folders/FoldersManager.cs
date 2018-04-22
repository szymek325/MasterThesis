using System.Threading.Tasks;
using Dropbox.Api;
using Dropbox.Api.Files;
using DropboxIntegration.Configuration;
using Microsoft.Extensions.Logging;

namespace DropboxIntegration.Folders
{
    public class FoldersManager : IFoldersManager
    {
        private readonly DropboxClient dbxClient;
        private readonly ILogger<FoldersManager> logger;

        public FoldersManager(ILogger<FoldersManager> logger)
        {
            dbxClient = DropboxClientFactory.GetDropboxClient();
            this.logger = logger;
        }

        public async Task<ListFolderResult> GetFolderContent(string path)
        {
            var response = await dbxClient.Files.ListFolderAsync(path);
            return response;
        }
    }
}