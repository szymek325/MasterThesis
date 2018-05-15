using System.Threading.Tasks;
using Dropbox.Api.Files;

namespace DropboxIntegration.Folders
{
    public interface IFoldersClient
    {
        Task<ListFolderResult> GetFolderContent(string folderPath);
    }
}