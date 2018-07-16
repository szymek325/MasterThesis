using System;
using System.Collections.Generic;
using System.IO;
using Domain.Files.DTO;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using PeopleUploader.Configuration;
using PeopleUploader.Models;
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

        public IEnumerable<ParsedFile> GetFiles()
        {
            var parsedFiles = new List<ParsedFile>();
            try
            {
                var filePaths = Directory.GetFiles(peopleConfiguration.Value.Path);
                logger.LogInformation($"Found {filePaths.Length} files in directory {peopleConfiguration.Value.Path}");
                foreach (var file in filePaths)
                {
                    var fileStream= new FileStream(file, FileMode.Open, FileAccess.Read);
                    var name = file.Split('\\');
                    var fileNameWithoutType = name[name.Length - 1];
                    var fileName = $"{fileNameWithoutType}.jpg";
                    var personName = fileNameWithoutType.Split('.')[0];
                    parsedFiles.Add(new ParsedFile
                    {
                        Name = fileName,
                        FileStream = fileStream,
                        PersonName = personName,
                        FilePath = file
                    });
                }
            }
            catch (Exception ex)
            {
                logger.LogError("exception when looking for files",ex);
            }
            return parsedFiles;
        }
    }
}
