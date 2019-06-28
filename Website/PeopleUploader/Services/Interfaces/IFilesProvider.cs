using System.Collections.Generic;
using PeopleUploader.Models;

namespace PeopleUploader.Services.Interfaces
{
    public interface IFilesProvider
    {
        IEnumerable<ParsedFile> GetFiles();
    }
}