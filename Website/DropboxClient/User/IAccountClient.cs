using System.Threading.Tasks;
using Dropbox.Api.Users;

namespace Dropbox.Client.User
{
    public interface IAccountClient
    {
        Task<FullAccount> GetAccountDataAsync();
    }
}