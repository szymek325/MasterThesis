using System.IO;
using System.Threading.Tasks;

namespace DropboxIntegration.Files
{
    public interface IFilesClient
    {
        Task Upload(string folder, string file, Stream content);
        Task<string> DownloadAsString(string folder, string file);
        Task<Stream> DownloadAsStream(string folder, string file);
        Task<string> DownloadThumbnail(string folder, string file);
    }
}