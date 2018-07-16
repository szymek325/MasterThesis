using System;
using Microsoft.Extensions.DependencyInjection;
using NLog;
using PeopleUploader.Configuration;

namespace PeopleUploader
{
    internal class Program
    {
        private static void Main(string[] args)
        {
            var servicesProvider = DependencyInjector.BuildDi();
            var runner = servicesProvider.GetRequiredService<IRunner>();
            runner.DoAction("Action1");

            Console.WriteLine("Press ANY key to exit");
            Console.ReadLine();

            // Ensure to flush and stop internal timers/threads before application-exit (Avoid segmentation fault on Linux)
            LogManager.Shutdown();
        }
    }
}