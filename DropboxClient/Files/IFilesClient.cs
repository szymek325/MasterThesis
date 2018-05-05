using System.IO;
using System.Threading.Tasks;

namespace DropboxIntegration.Files
{
    public interface IFilesClient
    {
        Task Upload(string folder, string file, Stream content);
        Task Delete(string folder, string file = null);
        Task<string> DownloadThumbnail(string folder, string file);
    }
}