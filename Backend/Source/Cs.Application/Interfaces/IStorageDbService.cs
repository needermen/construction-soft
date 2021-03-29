using Cs.Domain.Files;

namespace Cs.Application.Interfaces
{
    public interface IStorageDbService : IDbService
    {
        IRepository<File> Files { get; set; }
    }
}