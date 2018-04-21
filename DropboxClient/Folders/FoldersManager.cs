using System.Threading.Tasks;
using Dropbox.Api;
using Dropbox.Api.Files;
using Microsoft.Extensions.Logging;

namespace DropboxIntegration.Folders
{
    public class FoldersManager : IFoldersManager
    {
        private readonly DropboxClient dbxClient;
        private readonly ILogger<FoldersManager> logger;

        public FoldersManager(DropboxClient dbxClient, ILogger<FoldersManager> logger)
        {
            this.dbxClient = dbxClient;
            this.logger = logger;
        }

        public async Task<ListFolderResult> GetFolderContent(string path)
        {
            var response = await dbxClient.Files.ListFolderAsync(path);
            return response;
        }
    }
}