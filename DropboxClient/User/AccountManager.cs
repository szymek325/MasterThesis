using System.Threading.Tasks;
using Dropbox.Api;
using Dropbox.Api.Users;
using DropboxIntegration.Configuration;
using Microsoft.Extensions.Logging;

namespace DropboxIntegration.User
{
    public class AccountManager : IAccountManager
    {
        private readonly DropboxClient dbClient;
        private readonly ILogger<AccountManager> logger;

        public AccountManager(ILogger<AccountManager> logger)
        {
            this.logger = logger;
            dbClient = DropboxClientFactory.GetDropboxClient();
        }

        public async Task<FullAccount> GetAccountDataAsync()
        {
            var fullAccountInfo = await dbClient.Users.GetCurrentAccountAsync();
            logger.LogInformation($"Logges as user {fullAccountInfo.Name}");
            return fullAccountInfo;
        }
    }
}