using Dropbox.Api;

namespace DropboxIntegration.Configuration
{
    public static class DropboxClientFactory
    {
        private static readonly DropboxClient client;

        static DropboxClientFactory()
        {
            client = new DropboxClient("bJ90jq_k1TAAAAAAAAAABiHGq8c16qGnRew7tKYN1yJdChP3CRiPIpOG30ExjVkZ");
        }

        public static DropboxClient GetDropboxClient()
        {
            return client;
        }
    }
}