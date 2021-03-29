using System.Collections.Generic;
using System.Linq;
using AutoMapper;
using Cs.Application.Buildings.Models;
using Cs.Application.Interfaces;
using Cs.Application.Tools;
using Cs.Common.Models;
using Cs.Domain.Buildings;
using Microsoft.EntityFrameworkCore;

namespace Cs.Application.Buildings
{
    public class WorkTechnicService : IDependedCrudOperation<WorkTechnicViewModel>
    {
        private readonly IBuildingDbService _db;
        private readonly ITechnicDbService _technicDb;
        private readonly IMapper _mapper;

        public WorkTechnicService(IBuildingDbService db, ITechnicDbService technicDb, IMapper mapper)
        {
            _db = db;
            _technicDb = technicDb;
            _mapper = mapper;
        }

        public ListResult<WorkTechnicViewModel> Get(int workId, Paging paging)
        {
            var work = _db.Works.All()
                .Include(w => w.Technics)
                .ThenInclude(bm => bm.Technic)
                .ThenInclude(bm => bm.Category)
                .Include(w => w.Technics)
                .ThenInclude(bm => bm.Technic)
                .ThenInclude(bm => bm.Dimension)
                .FirstOrDefault(w => w.Id == workId);
            if (work == null)
                throw new ServiceException(UiConstants.ObjectNotFound);

            var mainTechnics = work.Technics;
            if (!string.IsNullOrEmpty(paging.Search))
                mainTechnics = mainTechnics.Where(t => t.Technic.Name.Contains(paging.Search))
                    .ToList();

            var result = mainTechnics.Skip(paging.Skip).Take(paging.Count).ToList();
            var total = mainTechnics.Count();

            var viewModels = _mapper.Map<List<WorkTechnicViewModel>>(result);

            return new ListResult<WorkTechnicViewModel>(viewModels, total);
        }

        public WorkTechnicViewModel Get(int workId, int id)
        {
            var work = _db.Works.All()
                .Include(w => w.Technics)
                .ThenInclude(bm => bm.Technic)
                .ThenInclude(bm => bm.Category)
                .Include(w => w.Technics)
                .ThenInclude(bm => bm.Technic)
                .ThenInclude(bm => bm.Dimension)
                .FirstOrDefault(w => w.Id == workId);

            if (work == null)
                throw new ServiceException(UiConstants.ObjectNotFound);

            var mainTechnic = work.Technics.FirstOrDefault(bm => bm.TechnicId == id);
            if (mainTechnic == null)
                throw new ServiceException(UiConstants.ObjectNotFound);

            return _mapper.Map<WorkTechnicViewModel>(mainTechnic);
        }

        public int Add(int workId, WorkTechnicViewModel t)
        {
            var work = _db.Works.All()
                .Include(w => w.Technics)
                .ThenInclude(bm => bm.Technic)
                .FirstOrDefault(w => w.Id == workId);

            if (work == null)
                throw new ServiceException(UiConstants.ObjectNotFound);

            var dbTechnic = work.Technics.FirstOrDefault(bm => bm.TechnicId == t.TechnicId);
            if (dbTechnic == null)
            {
                var workTechnic = _mapper.Map<WorkTechnic>(t);
                workTechnic.Technic = _technicDb.Technics.Get(workTechnic.TechnicId);

                work.Technics.Add(workTechnic);
            }
            else
            {
                dbTechnic.Count += t.Count;
            }

            _updatePrices(work);

            _db.Save();

            return 1;
        }

        public void Update(int workId, WorkTechnicViewModel t)
        {
            var work = _db.Works.All()
                .Include(w => w.Technics)
                .ThenInclude(bm => bm.Technic)
                .FirstOrDefault(w => w.Id == workId);

            if (work == null)
                throw new ServiceException(UiConstants.ObjectNotFound);

            var dbTechnic = work.Technics.FirstOrDefault(bm => bm.TechnicId == t.TechnicId);
            if (dbTechnic == null)
                throw new ServiceException(UiConstants.ObjectNotFound);

            _mapper.Map(t, dbTechnic);

            _updatePrices(work);

            _db.Save();
        }

        public void Delete(int workId, int id)
        {
            var work = _db.Works.All()
                .Include(w => w.Technics)
                .ThenInclude(bm => bm.Technic)
                .FirstOrDefault(w => w.Id == workId);

            if (work == null)
                throw new ServiceException(UiConstants.ObjectNotFound);

            var dbTechnic = work.Technics.FirstOrDefault(bm => bm.TechnicId == id);
            if (dbTechnic == null)
                throw new ServiceException(UiConstants.ObjectNotFound);

            work.Technics.Remove(dbTechnic);

            _updatePrices(work);

            _db.Save();
        }

        private void _updatePrices(Work work)
        {
            var category = _db.WorkCategories.Get(work.WorkCategoryId);
            var phase = _db.Phases.Get(category.PhaseId);
            var building = _db.Buildings.Get(phase.BuildingId);

            building.FullPrice -= phase.FullPrice;
            phase.FullPrice -= category.FullPrice;
            category.FullPrice -= work.FullPrice;

            work.PriceForTechnics = work.Technics.Sum(bm => bm.Count * bm.Technic.Price * bm.Technic.Coefficient);

            category.FullPrice += work.FullPrice;
            phase.FullPrice += category.FullPrice;
            building.FullPrice += phase.FullPrice;
        }
    }
}