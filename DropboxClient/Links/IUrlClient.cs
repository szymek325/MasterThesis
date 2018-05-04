using System.Collections.Generic;
using System.Threading.Tasks;
using Dropbox.Api.Sharing;

namespace DropboxIntegration.Links
{
    public interface IUrlClient
    {
        Task<string> CreateLinkToFile(string pathToFile);
        Task<IEnumerable<SharedLinkMetadata>> GetAllLinks();
        Task<IEnumerable<SharedLinkMetadata>> GetAllLinksFromFolder(string pathToFolder);
    }
}