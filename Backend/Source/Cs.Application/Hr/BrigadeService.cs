using System.Collections.Generic;
using System.Linq;
using AutoMapper;
using Cs.Application.Hr.Models;
using Cs.Application.Interfaces;
using Cs.Application.Tools;
using Cs.Common.Models;
using Cs.Domain.Hr;
using Microsoft.EntityFrameworkCore;

namespace Cs.Application.Hr
{
    public class BrigadeService : ICrudOperation<BrigadeViewModel>
    {
        private readonly IHrDbService _db;
        private readonly IMapper _mapper;

        public BrigadeService(IHrDbService db, IMapper mapper)
        {
            _db = db;
            _mapper = mapper;
        }

        public ListResult<BrigadeViewModel> Get(Paging paging)
        {
            var brigades = _db.Brigades.All();
            if (!string.IsNullOrEmpty(paging.Search))
                brigades = brigades.Where(t => t.Name.Contains(paging.Search)
                                               || t.PaymentType.Name.Contains(paging.Search)
                                               || t.Category.Name.Contains(paging.Search));

            var result = brigades.Include(t => t.Category).Include(t => t.PaymentType).Skip(paging.Skip)
                .Take(paging.Count).ToList();
            
            var total = brigades.Count();

            var viewModels = _mapper.Map<List<BrigadeViewModel>>(result);

            return new ListResult<BrigadeViewModel>(viewModels, total);
        }

        public BrigadeViewModel Get(int id)
        {
            var brigade = _db.Brigades.All()
                .Include(b => b.Files)
                .ThenInclude(f => f.File)
                .FirstOrDefault(b => b.Id == id);

            return _mapper.Map<BrigadeViewModel>(brigade);
        }

        public int Add(BrigadeViewModel t)
        {
            if (_db.Brigades.All().Any(bc => bc.Name == t.Name))
                throw new ServiceException(UiConstants.EntityWithSameNameAlreadyExists(t.Name));

            var dbBrigade = _mapper.Map<Brigade>(t);

            _db.Brigades.Add(dbBrigade);
            _db.Brigades.Save();

            return dbBrigade.Id;
        }

        public void Update(BrigadeViewModel t)
        {
            if (_db.Brigades.All().Any(bc => bc.Name == t.Name && bc.Id != t.Id))
                throw new ServiceException(UiConstants.EntityWithSameNameAlreadyExists(t.Name));

            var brigade = _db.Brigades.All()
                .Include(b => b.Files)
                .FirstOrDefault(b => b.Id == t.Id);
            
            if (brigade == null)
                throw new ServiceException(UiConstants.ObjectNotFound);

            _mapper.Map(t, brigade);

            _db.Brigades.Update(brigade);
            _db.Brigades.Save();
        }

        public void Delete(int id)
        {
            _db.Brigades.Delete(id);
            _db.Brigades.Save();
        }
    }
}