using System;
using System.Threading.Tasks;
using DropboxIntegration;
using Microsoft.Extensions.DependencyInjection;

namespace ConsoleAppForTestingPurposes
{
    internal class Program
    {
        private static void Main(string[] args)
        {
            var service = new ServiceCollection();
            service.AddDropboxConnector();

            var provider = service.BuildServiceProvider();

            var task = Task.Run(Run);
            task.Wait();

            var dropbox = provider.GetService<IFilesUploader>();


            dropbox.GetAccountData();
            Console.ReadKey();
            
            Console.WriteLine("Hello World!");
        }

        private static async Task Run()
        {
            using (var dbx =
                new Dropbox.Api.DropboxClient("qDKRP9Nt71gAAAAAAAAkfWWHoeroKQ9X9-ek-KuXO_PfG_Elip-UMP2acgUY_3C9"))
            {
                var full = await dbx.Users.GetCurrentAccountAsync();
                Console.WriteLine("{0} - {1}", full.Name.DisplayName, full.Email);
            }
        }
    }
}