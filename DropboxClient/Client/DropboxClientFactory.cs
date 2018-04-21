using Dropbox.Api;

namespace DropboxIntegration.Client
{
    public static class DropboxClientFactory
    {
        private static readonly DropboxClient client;

        static DropboxClientFactory()
        {
            client = new DropboxClient("qDKRP9Nt71gAAAAAAAAkfWWHoeroKQ9X9-ek-KuXO_PfG_Elip-UMP2acgUY_3C9");
        }

        public static DropboxClient GetDropboxClient()
        {
            return client;
        }
    }
}