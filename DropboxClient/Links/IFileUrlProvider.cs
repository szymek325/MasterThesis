using System.Threading.Tasks;

namespace DropboxIntegration.Links
{
    public interface IFileUrlProvider
    {
        Task<string> GetLink(string pathToFile);
    }
}