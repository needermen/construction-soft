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
    public class PhaseService : IPhaseService
    {
        private readonly IBuildingDbService _db;
        private readonly IMapper _mapper;

        public PhaseService(IBuildingDbService db, IMapper mapper)
        {
            _db = db;
            _mapper = mapper;
        }

        public ListResult<PhaseViewModel> Get(Paging paging)
        {
            var phases = _db.Phases.All();
            if (!string.IsNullOrEmpty(paging.Search))
                phases = phases.Where(t => t.Name.Contains(paging.Search));

            var result = phases.Skip(paging.Skip).Take(paging.Count).ToList();
            var total = phases.Count();

            var viewModels = _mapper.Map<List<PhaseViewModel>>(result);

            return new ListResult<PhaseViewModel>(viewModels, total);
        }
        
        public ListResult<PhaseViewModel> Get(Paging paging, int orgId)
        {
            var phases = _db.Phases.All().Where(ph => ph.OrganizationId == orgId);
            if (!string.IsNullOrEmpty(paging.Search))
                phases = phases.Where(t => t.Name.Contains(paging.Search));

            var result = phases.Skip(paging.Skip).Take(paging.Count).ToList();
            var total = phases.Count();

            var viewModels = _mapper.Map<List<PhaseViewModel>>(result);

            return new ListResult<PhaseViewModel>(viewModels, total);
        }

        public PhaseViewModel Get(int id)
        {
            var phase = _db.Phases.Get(id);

            return _mapper.Map<PhaseViewModel>(phase);
        }

        public int Add(PhaseViewModel t)
        {
            var buildingPhases = _db.Phases.All().Where(p => p.BuildingId == t.BuildingId);
            if (buildingPhases.Any(bc => bc.Name == t.Name))
                throw new ServiceException(UiConstants.EntityWithSameNameAlreadyExists(t.Name));

            var phase = _mapper.Map<Phase>(t);

            _db.Phases.Add(phase);
            _db.Phases.Save();

            return phase.Id;
        }

        public void Update(PhaseViewModel t)
        {
            var buildingPhases = _db.Phases.All().Where(p => p.BuildingId == t.BuildingId);
            if (buildingPhases.Any(bc => bc.Name == t.Name && t.Id != bc.Id))
                throw new ServiceException(UiConstants.EntityWithSameNameAlreadyExists(t.Name));

            var phase = _db.Phases.Get(t.Id);
            _mapper.Map(t, phase);

            _db.Phases.Update(phase);
            _db.Phases.Save();
        }

        public void Delete(int id)
        {
            _db.Phases.Delete(id);
            _db.Phases.Save();
        }

        public ListResult<PhaseViewModel> GetByBuilding(int buildingId)
        {
            var phases = _db.Phases.All()
                .Where(p => p.BuildingId == buildingId)
                .OrderBy(c => c.StartDate)
                .ThenBy(c => c.EndDate)
                .ThenBy(c => c.CreatedDate).ToList();

            var total = phases.Count();

            var viewModels = _mapper.Map<List<PhaseViewModel>>(phases);

            return new ListResult<PhaseViewModel>(viewModels, total);
        }

        public ListResult<DateRangeViewModel> GetDateRanges(int id)
        {
            var phase = _db.Phases.All().Include(c => c.WorkCategories).FirstOrDefault(c => c.Id == id);
            if (phase != null)
            {
                var dateRanges = phase.WorkCategories.OrderBy(c => c.StartDate).Select(w => new DateRangeViewModel
                {
                    Start = w.StartDate?.ToString("d"),
                    End = w.EndDate?.ToString("d"),
                    Title = w.Name
                }).ToList();

                return new ListResult<DateRangeViewModel>(dateRanges, dateRanges.Count);
            }

            throw new ServiceException(UiConstants.ObjectNotFound);
        }
    }
}