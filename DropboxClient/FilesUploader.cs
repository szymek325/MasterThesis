using System.Threading.Tasks;
using Dropbox.Api;
using Dropbox.Api.Users;
using DropboxIntegration.Client;

namespace DropboxIntegration
{
    public class FilesUploader : IFilesUploader
    {
        private readonly DropboxClient dbClient;

        public FilesUploader()
        {
            dbClient = DropboxClientFactory.GetDropboxClient();
        }

        public async Task<FullAccount> GetAccountData()
        {
            var fullAccountInfo = await dbClient.Users.GetCurrentAccountAsync();
            return fullAccountInfo;
        }
    }
}