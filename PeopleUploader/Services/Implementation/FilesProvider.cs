using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using PeopleUploader.Configuration;
using PeopleUploader.Services.Interfaces;

namespace PeopleUploader.Services.Implementation
{
    public class FilesProvider : IFilesProvider
    {
        private readonly ILogger<FilesProvider> logger;
        private readonly IOptions<PeopleConfiguration> peopleConfiguration;

        public FilesProvider(ILogger<FilesProvider> logger, IOptions<PeopleConfiguration> peopleConfiguration)
        {
            this.logger = logger;
            this.peopleConfiguration = peopleConfiguration;
        }

        public IEnumerable<FileInfo> GetFiles()
        {
            try
            {
                var files = Directory.GetFiles(peopleConfiguration.Value.Path);
            }
            catch (Exception ex)
            {
                logger.LogError("exception when looking for files",ex);
            }
            return new List<FileInfo>();
        }
    }
}
