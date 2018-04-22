using System.Collections.Generic;
using System.Threading.Tasks;
using Dropbox.Api.Sharing;

namespace DropboxIntegration.Links
{
    public interface IUrlManager
    {
        Task<string> CreateLinkToFile(string pathToFile);
        Task<IEnumerable<SharedLinkMetadata>> GetAllLinks(string pathToFolder=null);
    }
}