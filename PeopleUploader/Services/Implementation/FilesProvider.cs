using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
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
            var subdirectories = Directory.GetDirectories(peopleConfiguration.Value.Path);
            return subdirectories.Length != 0
                ? ParseFilesFromSubDirectories(subdirectories)
                : ParseFilesFromDirectory();
        }

        private IEnumerable<ParsedFile> ParseFilesFromSubDirectories(IEnumerable<string> foundDirectories)
        {
            var parsedFiles = new List<ParsedFile>();
            try
            {
                foreach (var directory in foundDirectories)
                {
                    var filePaths = Directory.GetFiles(directory);
                    var filteredList = GetFilteredList(filePaths).ToList();
                    Console.WriteLine($"Found {filePaths.Length}, taking {filteredList.Count()} files from directory {directory}");
                    logger.LogInformation($"Found {filePaths.Length}, taking {filteredList.Count()} files from directory {directory}");
                    var personName = directory.Split('\\').Last();
                    foreach (var file in filteredList)
                    {
                        var fileStream = new FileStream(file, FileMode.Open, FileAccess.Read);
                        var fileName = file.Split('\\').Last();
                        parsedFiles.Add(new ParsedFile
                        {
                            Name = fileName,
                            FileStream = fileStream,
                            PersonName = personName,
                            FilePath = file
                        });
                    }
                }
            }
            catch (Exception ex)
            {
                logger.LogError(ex, "exception when looking for files");
            }

            return parsedFiles;
        }

        private IEnumerable<ParsedFile> ParseFilesFromDirectory()
        {
            var parsedFiles = new List<ParsedFile>();
            try
            {
                var filePaths = Directory.GetFiles(peopleConfiguration.Value.Path);
                Console.WriteLine($"Found {filePaths.Length} files in directory {peopleConfiguration.Value.Path}");
                logger.LogInformation($"Found {filePaths.Length} files in directory {peopleConfiguration.Value.Path}");
                foreach (var file in filePaths.Take(peopleConfiguration.Value.MaxPhotosPerPerson))
                {
                    var fileStream = new FileStream(file, FileMode.Open, FileAccess.Read);
                    var name = file.Split('\\').Last();
                    var fileNameWithoutType = name.Replace('.', '_');
                    var fileName = $"{fileNameWithoutType}.jpg";
                    var personName = fileNameWithoutType.Split('_')[0];
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
                logger.LogError(ex, "exception when looking for files");
            }

            return parsedFiles;
        }

        private IEnumerable<string> GetFilteredList(IEnumerable<string> listToFilter)
        {
            return listToFilter.Where(x => x.ToLower().EndsWith(".jpg")
                                           || x.ToLower().EndsWith(".jpeg")
                                           || x.ToLower().EndsWith(".png"))
                .Take(peopleConfiguration.Value.MaxPhotosPerPerson);
        }
    }
}