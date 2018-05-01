using System.Collections.Generic;
using System.Threading.Tasks;
using Domain.Files.DTO;

namespace Domain.Files
{
    public interface IFilesDomainService
    {
        Task Upload(IEnumerable<FileToUpload> files, string location = "/reco");
        Task<FileToUpload> Download(string path, string fileName);
        Task<FileLink> GetLinkToFile(string path, string fileName);
        Task<IEnumerable<FileLink>> GetLinks(string folderPath);
    }
}