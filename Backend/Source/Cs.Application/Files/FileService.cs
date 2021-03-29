using AutoMapper;
using Cs.Application.Files.Models;
using Cs.Application.Interfaces;
using Cs.Domain.Files;

namespace Cs.Application.Files
{
    public class FileService : IFileService
    {
        private readonly IStorageDbService _db;
        private readonly IMapper _mapper;

        public FileService(IStorageDbService db, IMapper mapper)
        {
            _db = db;
            _mapper = mapper;
        }

        public File Get(int id)
        {
            return _db.Files.Get(id);
        }

        public FileViewModel[] Add(File[] files)
        {
            _db.Files.AddRange(files);
            _db.Files.Save();

            return _mapper.Map<FileViewModel[]>(files);
        }

        public void Delete(int id)
        {
            _db.Files.Delete(id);
            _db.Files.Save();
        }
    }
}