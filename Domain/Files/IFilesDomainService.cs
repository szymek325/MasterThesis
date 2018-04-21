using System.IO;
using System.Threading.Tasks;

namespace Domain.Files
{
    public interface IFilesDomainService
    {
        Task Upload(Stream formFileCollection);
    }
}