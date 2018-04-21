using System.Threading.Tasks;
using Dropbox.Api.Users;

namespace DropboxIntegration
{
    public interface IFilesUploader
    {
        Task<FullAccount> GetAccountData();
    }
}