using System.Threading.Tasks;
using Dropbox.Api;
using Dropbox.Api.Users;
using DropboxIntegration.Client;
using Microsoft.Extensions.Logging;

namespace DropboxIntegration.User
{
    public class AccountManager : IAccountManager
    {
        private readonly DropboxClient dbClient;
        private readonly ILogger logger;

        public AccountManager(ILogger logger)
        {
            this.logger = logger;
            dbClient = DropboxClientFactory.GetDropboxClient();

        }

        public async Task<FullAccount> GetAccountDataAsync()
        {
            var fullAccountInfo = await dbClient.Users.GetCurrentAccountAsync();
            return fullAccountInfo;
        }
    }
}