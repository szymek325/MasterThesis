using System.Threading.Tasks;
using Dropbox.Api.Users;

namespace DropboxIntegration.User
{
    public interface IAccountManager
    {
        Task<FullAccount> GetAccountDataAsync();
    }
}