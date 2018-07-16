using System.Collections.Generic;
using System.IO;

namespace PeopleUploader.Services.Interfaces
{
    public interface IFilesProvider
    {
        IEnumerable<FileInfo> GetFiles();
    }
}