using System.Collections.Generic;
using System.Threading.Tasks;
using Dropbox.Api.Files;

namespace DropboxIntegration.Folders
{
    public interface IFoldersManager
    {
        Task<ListFolderResult> GetFolderContent(string path);
    }
}