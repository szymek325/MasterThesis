using System.Collections.Generic;
using System.Threading.Tasks;
using DataLayer.Entities;
using DataLayer.Entities.Common;
using Domain.Files.DTO;

namespace Domain.Files
{
    public interface IFilesDomainService
    {
        Task Upload(IEnumerable<FileToUpload> files, string location);
        Task<IEnumerable<FileLink>> GetLinksToFilesInFolder(string folderPath);
        Task DeleteSingleFile(ImageAttachment file);
        Task DeleteFiles(IEnumerable<ImageAttachment> files);
        Task GetThumbnail(ImageAttachment file);
    }
}