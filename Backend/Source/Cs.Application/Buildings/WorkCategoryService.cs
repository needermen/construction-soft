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
    public class WorkCategoryService : IWorkCategoryService
    {
        private readonly IBuildingDbService _db;
        private readonly IMapper _mapper;

        public WorkCategoryService(IBuildingDbService db, IMapper mapper)
        {
            _db = db;
            _mapper = mapper;
        }

        public ListResult<WorkCategoryViewModel> Get(Paging paging)
        {
            var categories = _db.WorkCategories.All();
            if (!string.IsNullOrEmpty(paging.Search))
                categories = categories.Where(t => t.Name.Contains(paging.Search));

            var result = categories.Skip(paging.Skip).Take(paging.Count).ToList();
            var total = categories.Count();

            var viewModels = _mapper.Map<List<WorkCategoryViewModel>>(result);

            return new ListResult<WorkCategoryViewModel>(viewModels, total);
        }

        public ListResult<WorkCategoryViewModel> Get(Paging paging, int orgId)
        {
            var categories = _db.WorkCategories.All().Where(wc => wc.OrganizationId == orgId);
            if (!string.IsNullOrEmpty(paging.Search))
                categories = categories.Where(t => t.Name.Contains(paging.Search));

            var result = categories.Skip(paging.Skip).Take(paging.Count).ToList();
            var total = categories.Count();

            var viewModels = _mapper.Map<List<WorkCategoryViewModel>>(result);

            return new ListResult<WorkCategoryViewModel>(viewModels, total);
        }


        public WorkCategoryViewModel Get(int id)
        {
            var workCategory = _db.WorkCategories.Get(id);

            return _mapper.Map<WorkCategoryViewModel>(workCategory);
        }

        public int Add(WorkCategoryViewModel t)
        {
            var workingCategoriesOfPhase = _db.WorkCategories.All().Where(p => p.PhaseId == t.PhaseId);
            if (workingCategoriesOfPhase.Any(bc => bc.Name == t.Name))
                throw new ServiceException(UiConstants.EntityWithSameNameAlreadyExists(t.Name));

            var workCategory = _mapper.Map<WorkCategory>(t);

            _db.WorkCategories.Add(workCategory);
            _db.WorkCategories.Save();

            return workCategory.Id;
        }

        public void Update(WorkCategoryViewModel t)
        {
            var workingCategoriesOfPhase = _db.WorkCategories.All().Where(p => p.PhaseId == t.PhaseId);
            if (workingCategoriesOfPhase.Any(bc => bc.Name == t.Name && bc.Id != t.Id))
                throw new ServiceException(UiConstants.EntityWithSameNameAlreadyExists(t.Name));

            var workCategory = _db.WorkCategories.Get(t.Id);
            _mapper.Map(t, workCategory);

            _db.WorkCategories.Update(workCategory);
            _db.WorkCategories.Save();
        }

        public void Delete(int id)
        {
            _db.WorkCategories.Delete(id);
            _db.WorkCategories.Save();
        }

        public ListResult<WorkCategoryViewModel> GetByPhase(int phaseId)
        {
            var categories = _db.WorkCategories.All()
                .Where(p => p.PhaseId == phaseId)
                .OrderBy(c => c.StartDate)
                .ThenBy(c => c.EndDate)
                .ThenBy(c => c.CreatedDate).ToList();

            var total = categories.Count;

            var viewModels = _mapper.Map<List<WorkCategoryViewModel>>(categories);

            return new ListResult<WorkCategoryViewModel>(viewModels, total);
        }

        public ListResult<DateRangeViewModel> GetDateRanges(int id)
        {
            var category = _db.WorkCategories.All().Include(c => c.Works).FirstOrDefault(c => c.Id == id);
            if (category != null)
            {
                var dateRanges = category.Works.OrderBy(w => w.StartDate).Select(w => new DateRangeViewModel
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