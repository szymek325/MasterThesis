using System;
using System.Threading.Tasks;
using Microsoft.Extensions.DependencyInjection;
using NLog;
using PeopleUploader.Configuration;
using PeopleUploader.Services.Interfaces;

namespace PeopleUploader
{
    internal class Program
    {
        private static void Main(string[] args)
        {
            var servicesProvider = DependencyInjector.BuildDi();
            var runner = servicesProvider.GetRequiredService<IRunner>();
            MainAsync(runner).GetAwaiter().GetResult();

            // Ensure to flush and stop internal timers/threads before application-exit (Avoid segmentation fault on Linux)
            LogManager.Shutdown();
        }

        public static async Task MainAsync(IRunner programRunner)
        {
            await programRunner.Start();
            Console.WriteLine("Hello World!");
        }
    }
}