using System.Collections.Generic;
using System.Threading.Tasks;
using DataLayer.Entities;
using Domain.Files.DTO;

namespace Domain.Files
{
    public interface IFilesDomainService
    {
        Task Upload(IEnumerable<FileToUpload> files, string location);
        Task<IEnumerable<FileLink>> GetLinksToFilesInFolder(string folderPath);
        Task Delete(IEnumerable<File> files);
    }
}