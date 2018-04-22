using System.Threading.Tasks;

namespace DropboxIntegration.Links
{
    public interface IUrlManager
    {
        Task<string> CreateLinkToFile(string pathToFile);
    }
}