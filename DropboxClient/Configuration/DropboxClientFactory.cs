using Dropbox.Api;
using Microsoft.Extensions.Options;

namespace Dropbox.Client.Configuration
{
    public class DropboxClientFactory:IDropboxClientFactory
    {
        private readonly DropboxClient client;
        private readonly string basePath;

        public DropboxClientFactory(IOptions<DropboxConfiguration> dropboxConfiguration)
        {
            client = new DropboxClient(dropboxConfiguration.Value.ConnectionKey);
            basePath = dropboxConfiguration.Value.Path;
        }

        public DropboxClient GetDropboxClient()
        {
            return client;
        }

        public string GetBasePath()
        {
            return basePath;
        }
    }
}