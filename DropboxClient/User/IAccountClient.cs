using System.Threading.Tasks;
using Dropbox.Api.Users;

namespace DropboxIntegration.User
{
    public interface IAccountClient
    {
        Task<FullAccount> GetAccountDataAsync();
    }
}