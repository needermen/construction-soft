using Cs.Application.Files.Models;
using Cs.Domain.Files;

namespace Cs.Application.Files
{
    public interface IFileService
    {
        File Get(int id);
        FileViewModel[] Add(File[] t);
        void Delete(int id);
    }
}