using System.Threading.Tasks;
using Dropbox.Api.Files;

namespace Dropbox.Client.Folders
{
    public interface IFoldersClient
    {
        Task<ListFolderResult> GetFolderContent(string folderPath);
        Task DeleteFolder(string folder);
    }
}