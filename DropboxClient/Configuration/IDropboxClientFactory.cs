using Dropbox.Api;

namespace DropboxIntegration.Configuration
{
    public interface IDropboxClientFactory
    {
        DropboxClient GetDropboxClient();

        string GetBasePath();
    }
}