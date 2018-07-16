using System;
using System.IO;
using DataLayer.Configuration;
using Domain;
using Dropbox.Client.Configuration;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using NLog;
using NLog.Extensions.Logging;
using PeopleUploader.Configuration;
using LogLevel = NLog.LogLevel;

namespace PeopleUploader
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Hello World!");
            //setup our DI
            var logger = LogManager.LoadConfiguration("nlog.config").GetCurrentClassLogger();
        }

        private static IServiceProvider BuildDi()
        {
            //Configuration
            IConfigurationRoot config = new ConfigurationBuilder().SetBasePath(Directory.GetCurrentDirectory())
                .AddJsonFile(path: "appsettings.json", optional: false, reloadOnChange: true)
                .AddJsonFile(path: "appsettings.Azure.json", optional: false, reloadOnChange: true)
                .Build();

            //Runner is the custom class
            var services = new ServiceCollection();
            services.Configure<ConnectionStrings>(config.GetSection("ConnectionStrings"));
            services.Configure<DropboxConfiguration>(config.GetSection("DropboxConfiguration"));
            services.AddSingleton<ILoggerFactory, LoggerFactory>();
            services.AddSingleton(typeof(ILogger<>), typeof(Logger<>));
            services.AddTransient<IRunner, Runner>();
            
            services.AddDomainModule();

            var serviceProvider = services.BuildServiceProvider();

            var loggerFactory = serviceProvider.GetRequiredService<ILoggerFactory>();

            //configure NLog
            loggerFactory.AddNLog(new NLogProviderOptions { CaptureMessageTemplates = true, CaptureMessageProperties = true });
            NLog.LogManager.LoadConfiguration("nlog.config");

            return serviceProvider;
        }
    }
}
