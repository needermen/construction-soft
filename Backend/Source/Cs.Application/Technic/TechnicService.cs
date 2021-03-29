using System.Linq;
using Cs.Application.Interfaces;
using Cs.Application.Tools;
using Cs.Common.Models;
using Microsoft.EntityFrameworkCore;

namespace Cs.Application.Technic
{
    public class TechnicService : ICrudOperation<Domain.Technics.Technic>
    {
        private readonly ITechnicDbService _db;

        public TechnicService(ITechnicDbService db)
        {
            _db = db;
        }

        public ListResult<Domain.Technics.Technic> Get(Paging paging)
        {
            var technics = _db.Technics.All();
            if (!string.IsNullOrEmpty(paging.Search))
                technics = technics.Where(t => t.Name.Contains(paging.Search)
                                               || t.Comment.Contains(paging.Search)
                                               || t.Dimension.Name.Contains(paging.Search)
                                               || t.Category.Name.Contains(paging.Search));

            var result = technics.Include(t => t.Category).Include(t => t.Dimension).Skip(paging.Skip)
                .Take(paging.Count).ToList();
            var total = technics.Count();

            return new ListResult<Domain.Technics.Technic>(result, total);
        }

        public Domain.Technics.Technic Get(int id)
        {
            return _db.Technics.Get(id);
        }

        public int Add(Domain.Technics.Technic t)
        {
            if (_db.Technics.All().Any(bc => bc.Name == t.Name))
                throw new ServiceException(UiConstants.EntityWithSameNameAlreadyExists(t.Name));

            _db.Technics.Add(t);
            _db.Technics.Save();

            return t.Id;
        }

        public void Update(Domain.Technics.Technic t)
        {
            if (_db.Technics.All().Any(bc => bc.Name == t.Name && bc.Id != t.Id))
                throw new ServiceException(UiConstants.EntityWithSameNameAlreadyExists(t.Name));

            _db.Technics.Update(t);
            _db.Technics.Save();
        }

        public void Delete(int id)
        {
            _db.Technics.Delete(id);
            _db.Technics.Save();
        }
    }
}