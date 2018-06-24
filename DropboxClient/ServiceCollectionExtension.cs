using Dropbox.Client.Configuration;
using Dropbox.Client.Files;
using Dropbox.Client.Folders;
using Dropbox.Client.Links;
using Dropbox.Client.User;
using Microsoft.Extensions.DependencyInjection;

namespace Dropbox.Client
{
    public static class ServiceCollectionExtension
    {
        public static IServiceCollection AddDropboxConnector(this IServiceCollection services)
        {
            services.AddTransient<IDropboxClientFactory, DropboxClientFactory>();
            services.AddTransient<IFilesClient, FilesClient>();
            services.AddTransient<IFoldersClient, FoldersClient>();
            services.AddTransient<IAccountClient, AccountClient>();
            services.AddTransient<IUrlClient, UrlClient>();
            return services;
        }
    }
}