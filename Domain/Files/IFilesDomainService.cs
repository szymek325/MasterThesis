using System.Collections.Generic;
using System.Threading.Tasks;
using DropboxIntegration.Files.DTO;

namespace Domain.Files
{
    public interface IFilesDomainService
    {
        Task Upload(IEnumerable<FileToUpload> files);
    }
}