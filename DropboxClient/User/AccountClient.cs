using System.Threading.Tasks;
using Dropbox.Api;
using Dropbox.Api.Users;
using DropboxIntegration.Configuration;
using Microsoft.Extensions.Logging;

namespace DropboxIntegration.User
{
    public class AccountClient : IAccountClient
    {
        private readonly DropboxClient dbClient;
        private readonly ILogger<AccountClient> logger;

        public AccountClient(ILogger<AccountClient> logger)
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