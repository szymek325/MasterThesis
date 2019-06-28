using Dropbox.Api;

namespace Dropbox.Client.Configuration
{
    public interface IDropboxClientFactory
    {
        DropboxClient GetDropboxClient();

        string GetBasePath();
    }
}