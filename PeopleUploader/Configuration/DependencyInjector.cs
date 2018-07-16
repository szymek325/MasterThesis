using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using AutoMapper;
using DataLayer.Configuration;
using Domain;
using Dropbox.Client.Configuration;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using NLog;
using NLog.Extensions.Logging;

namespace PeopleUploader.Configuration
{
    public static class DependencyInjector
    {
        public static IServiceProvider BuildDi()
        {
            //Configuration
            var config = new ConfigurationBuilder().SetBasePath(Directory.GetCurrentDirectory())
                .AddJsonFile("appsettings.json", true, true)
                .AddJsonFile("appsettings.Azure.json", true, true)
                .Build();

            //Runner is the custom class
            var services = new ServiceCollection();
            services.AddAutoMapper();
            services.AddOptions();
            services.Configure<ConnectionStrings>(x => config.GetSection("ConnectionStrings").Bind(x));
            services.Configure<DropboxConfiguration>(x => config.GetSection("DropboxConfiguration").Bind(x));
            services.Configure<PeopleConfiguration>(x => config.GetSection("PeopleConfiguration").Bind(x));
            services.AddSingleton<ILoggerFactory, LoggerFactory>();
            services.AddSingleton(typeof(ILogger<>), typeof(Logger<>));
            services.AddTransient<IRunner, Runner>();
            services.AddDomainModule();

            var serviceProvider = services.BuildServiceProvider();

            //configure NLog
            var loggerFactory = serviceProvider.GetRequiredService<ILoggerFactory>();
            loggerFactory.AddNLog(new NLogProviderOptions
            {
                CaptureMessageTemplates = true,
                CaptureMessageProperties = true
            });
            LogManager.LoadConfiguration("nlog.config");

            return serviceProvider;
        }
    }
}
