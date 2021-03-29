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
    public class BuildingService : IBuildingService
    {
        private readonly IBuildingDbService _db;
        private readonly IMapper _mapper;

        public BuildingService(IBuildingDbService db, IMapper mapper)
        {
            _db = db;
            _mapper = mapper;
        }

        public ListResult<BuildingViewModel> Get(Paging paging)
        {
            var buildings = _db.Buildings.All();
            if (!string.IsNullOrEmpty(paging.Search))
                buildings = buildings.Where(t =>
                    t.Name.Contains(paging.Search) || t.Status.ToString().Contains(paging.Search));

            var result = buildings.Skip(paging.Skip).Take(paging.Count).ToList();
            var total = buildings.Count();

            var viewModels = _mapper.Map<List<BuildingViewModel>>(result);

            return new ListResult<BuildingViewModel>(viewModels, total);
        }

        public ListResult<BuildingViewModel> Get(Paging paging, int organizationId)
        {
            var buildings = _db.Buildings.All().Where(b => b.OrganizationId == organizationId);
            if (!string.IsNullOrEmpty(paging.Search))
            {
                buildings = buildings.Where(t =>
                    t.Name.Contains(paging.Search) || t.Status.ToString().Contains(paging.Search));
            }

            var result = buildings.Skip(paging.Skip).Take(paging.Count).ToList();
            var total = buildings.Count();

            var viewModels = _mapper.Map<List<BuildingViewModel>>(result);

            return new ListResult<BuildingViewModel>(viewModels, total);
        }

        public BuildingViewModel Get(int id)
        {
            var building = _db.Buildings.Get(id);

            return _mapper.Map<BuildingViewModel>(building);
        }

        public int Add(BuildingViewModel t)
        {
            if (_db.Buildings.All().Any(bc => bc.Name == t.Name))
                throw new ServiceException(UiConstants.EntityWithSameNameAlreadyExists(t.Name));

            var building = _mapper.Map<Building>(t);

            _db.Buildings.Add(building);
            _db.Buildings.Save();

            return building.Id;
        }

        public void Update(BuildingViewModel t)
        {
            if (_db.Buildings.All().Any(bc => bc.Name == t.Name && bc.Id != t.Id))
                throw new ServiceException(UiConstants.EntityWithSameNameAlreadyExists(t.Name));

            var building = _db.Buildings.Get(t.Id);
            if (building.Status != BuildingStatus.ახალი)
            {
                //TODO constant should be variable
                throw new ServiceException("ობიექტის რედაქტირება არ შეიძლება, რადგან ის მიმდინარეა ან დამთავრებული");
            }

            _mapper.Map(t, building);

            _db.Buildings.Update(building);
            _db.Buildings.Save();
        }

        public void Delete(int id)
        {
            _db.Buildings.Delete(id);
            _db.Buildings.Save();
        }

        public ListResult<DateRangeViewModel> GetDateRanges(int id)
        {
            var building = _db.Buildings.All().Include(c => c.Phases).FirstOrDefault(c => c.Id == id);
            if (building != null)
            {
                var dateRanges = building.Phases.OrderBy(ph => ph.StartDate).Select(w => new DateRangeViewModel
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