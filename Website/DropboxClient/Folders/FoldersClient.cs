using System;
using System.Threading.Tasks;
using Dropbox.Api;
using Dropbox.Api.Files;
using Dropbox.Client.Configuration;
using Microsoft.Extensions.Logging;

namespace Dropbox.Client.Folders
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

        public async Task DeleteFolder(string folder)
        {
            var path = $"{basePath}/{folder}";
            var result = await dbxClient.Files.DeleteV2Async(path);
            if (result.Metadata.IsDeleted)
            {
                logger.LogError($"Files were not deleted from {path}");
                throw new Exception("Files were not deleted.");
            }
        }
    }
}