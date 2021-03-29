using Cs.Application.Interfaces;
using Cs.Domain.Files;

namespace Cs.Persistence.Services
{
    public class StorageDbService : IStorageDbService
    {   
        public IRepository<File> Files { get; set; }

        public StorageDbService(IRepository<File> files)
        {
            Files = files;
        }
        
        public void Save()
        {
            Files.Save();
        }
    }
}