using System.Threading.Tasks;
using Dropbox.Api;
using Dropbox.Api.Users;
using DropboxIntegration.Client;

namespace DropboxIntegration.User
{
    public class AccountManager : IAccountManager
    {
        private readonly DropboxClient dbClient;

        public AccountManager()
        {
            dbClient = DropboxClientFactory.GetDropboxClient();
        }

        public async Task<FullAccount> GetAccountDataAsync()
        {
            var fullAccountInfo = await dbClient.Users.GetCurrentAccountAsync();
            return fullAccountInfo;
        }
    }
}