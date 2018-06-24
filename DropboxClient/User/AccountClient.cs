using System.Threading.Tasks;
using Dropbox.Api;
using Dropbox.Api.Users;
using DropboxIntegration.Configuration;
using Microsoft.Extensions.Logging;

namespace DropboxIntegration.User
{
    public class AccountClient : IAccountClient
    {
        private readonly DropboxClient dbxClient;
        private readonly ILogger<AccountClient> logger;
        private readonly IDropboxClientFactory dropboxClientFactory;

        public AccountClient(ILogger<AccountClient> logger, IDropboxClientFactory dropboxClientFactory)
        {
            this.logger = logger;
            this.dropboxClientFactory = dropboxClientFactory;
            dbxClient = this.dropboxClientFactory.GetDropboxClient();
        }

        public async Task<FullAccount> GetAccountDataAsync()
        {
            var fullAccountInfo = await dbxClient.Users.GetCurrentAccountAsync();
            logger.LogInformation($"Logges as user {fullAccountInfo.Name}");
            return fullAccountInfo;
        }
    }
}