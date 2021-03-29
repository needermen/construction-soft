using System;
using System.Collections.Generic;
using System.Linq;
using AutoMapper;
using Cs.Application.Buildings.Models;
using Cs.Application.Interfaces;
using Cs.Application.Tools;
using Cs.Common.Models;
using Cs.Domain;
using Cs.Domain.Buildings;
using Microsoft.EntityFrameworkCore;

namespace Cs.Application.Buildings
{
    public class WorkService : IWorkService
    {
        private readonly IBuildingDbService _db;
        private readonly IMapper _mapper;

        public WorkService(IBuildingDbService db, IMapper mapper)
        {
            _db = db;
            _mapper = mapper;
        }

        public ListResult<WorkViewModel> Get(Paging paging)
        {
            var works = _db.Works.All();
            if (!string.IsNullOrEmpty(paging.Search?.Trim()))
                works = works.Where(t => t.Name.Contains(paging.Search));

            var result = works.Skip(paging.Skip).Take(paging.Count).ToList();
            var total = works.Count();

            var viewModels = _mapper.Map<List<WorkViewModel>>(result);

            return new ListResult<WorkViewModel>(viewModels, total);
        }

        public ListResult<WorkViewModel> Get(Paging paging, int orgId)
        {
            var works = _db.Works.All().Where(w => w.OrganizationId == orgId);
            if (!string.IsNullOrEmpty(paging.Search?.Trim()))
                works = works.Where(t => t.Name.Contains(paging.Search));

            var result = works.Skip(paging.Skip).Take(paging.Count).ToList();
            var total = works.Count();

            var viewModels = _mapper.Map<List<WorkViewModel>>(result);

            return new ListResult<WorkViewModel>(viewModels, total);
        }

        public WorkViewModel Get(int id)
        {
            var work = _db.Works.All().Include(w => w.HasToBeDoneAfter).FirstOrDefault(w => w.Id == id);
            if (work == null)
                throw new ServiceException(UiConstants.ObjectNotFound);

            return _mapper.Map<WorkViewModel>(work);
        }

        public int Add(WorkViewModel t)
        {
            var worksOfCategory = _db.Works.All().Where(p => p.WorkCategoryId == t.WorkCategoryId);
            if (worksOfCategory.Any(bc => bc.Name == t.Name))
                throw new ServiceException(UiConstants.EntityWithSameNameAlreadyExists(t.Name));

            var work = _mapper.Map<Work>(t);

            _validate(work);

            _db.Works.Add(work);

            _updateRelateObjects(work);

            _db.Save();

            return work.Id;
        }

        public void Update(WorkViewModel t)
        {
            var worksOfCategory = _db.Works.All().Where(p => p.WorkCategoryId == t.WorkCategoryId);
            if (worksOfCategory.Any(bc => bc.Name == t.Name && bc.Id != t.Id))
                throw new ServiceException(UiConstants.EntityWithSameNameAlreadyExists(t.Name));

            var work = _db.Works.Get(t.Id);
            _mapper.Map(t, work);

            _validate(work);

            _db.Works.Update(work);

            _updateRelateObjects(work);

            _db.Save();
        }

        public void Delete(int id)
        {
            var work = _db.Works.Get(id);

            work.StartDate = DateTime.MaxValue;
            work.EndDate = DateTime.MinValue;

            _db.Works.Delete(id);

            _updateRelateObjects(work);

            _db.Save();
        }

        public ListResult<WorkViewModel> GetByWorkCategory(int workCategoryId)
        {
            var works = _db.Works.All()
                .Where(p => p.WorkCategoryId == workCategoryId)
                .OrderBy(c => c.StartDate)
                .ThenBy(c => c.EndDate)
                .ThenBy(c => c.CreatedDate).ToList();

            var total = works.Count;

            var viewModels = _mapper.Map<List<WorkViewModel>>(works);

            return new ListResult<WorkViewModel>(viewModels, total);
        }

        private void _validate(Work work)
        {
            if (work.StartDate > work.EndDate)
                throw new ServiceException("დაწყების თარიღი არ შეიძლება დასრულების თარიღზე მეტი იყოს");

            if (work.StartDate == work.EndDate)
                throw new ServiceException("დაწყების და დასრულების თარიღები უნდა განსხვავდებოდეს");

            if (work.HasToBeDoneAfterId != null)
            {
                var id = work.HasToBeDoneAfterId.Value;

                while (work.HasToBeDoneAfterId != null)
                {
                    work = _db.Works.Get(work.HasToBeDoneAfterId.Value);
                    if (work.HasToBeDoneAfterId == id)
                    {
                        throw new ServiceException(
                            "შეიქმნა დამოკიდებული სამუშაოების ციკლი, რაც დაუშვებელი, გთხოვთ შეცვალოთ დამოკიდებული სამუშაო");
                    }
                }
            }
        }

        public ListResult<DateRangeViewModel> GetDateRanges(int id)
        {
            var work = Get(id);

            var dateRange = new DateRangeViewModel
            {
                Start = work.StartDate?.ToString("d"),
                End = work.EndDate?.ToString("d"),
                Title = work.Name
            };

            var dates = new List<DateRangeViewModel> {dateRange};

            return new ListResult<DateRangeViewModel>(dates, 1);
        }

        private void _updateRelateObjects(Work work)
        {
            var works = _db.Works.All().Where(w => w.WorkCategoryId == work.WorkCategoryId && w.Id != work.Id);
            var startDate = works.Any()
                ? Min(works.Min(w => w.StartDate), work.StartDate)
                : (work.EntityStatus == EntityStatus.Deleted ? null : work.StartDate);
            var endDate = works.Any()
                ? Max(works.Max(w => w.EndDate), work.EndDate)
                : (work.EntityStatus == EntityStatus.Deleted ? null : work.EndDate);

            var fullPrice = works.Sum(w => w.FullPrice) +
                            (work.EntityStatus == EntityStatus.Deleted ? 0 : work.FullPrice);
            var extraPrice = works.Sum(w => w.ExtraPrice) +
                             (work.EntityStatus == EntityStatus.Deleted ? 0 : work.ExtraPrice);

            var category = _db.WorkCategories.Get(work.WorkCategoryId);
            category.StartDate = startDate;
            category.EndDate = endDate;
            category.FullPrice = fullPrice;
            category.ExtraPrice = extraPrice;
            category.DurationInDays = (category.EndDate - category.StartDate)?.Days;

            _db.WorkCategories.Update(category);

            var categories = _db.WorkCategories.All().Where(c => c.PhaseId == category.PhaseId && c.Id != category.Id);

            startDate = categories.Any()
                ? Min(categories.Min(w => w.StartDate), category.StartDate)
                : category.StartDate;
            endDate = categories.Any() ? Max(categories.Max(w => w.EndDate), category.EndDate) : category.EndDate;

            var phase = _db.Phases.Get(category.PhaseId);
            phase.StartDate = startDate;
            phase.EndDate = endDate;
            phase.FullPrice = categories.Sum(c => c.FullPrice) + category.FullPrice;
            phase.ExtraPrice = categories.Sum(c => c.ExtraPrice) + category.ExtraPrice;
            phase.DurationInDays = (phase.EndDate - phase.StartDate)?.Days;

            _db.Phases.Update(phase);

            var phases = _db.Phases.All().Where(p => p.BuildingId == phase.BuildingId && p.Id != phase.Id);
            startDate = phases.Any() ? Min(phases.Min(w => w.StartDate), phase.StartDate) : phase.StartDate;
            endDate = phases.Any() ? Max(phases.Max(w => w.EndDate), phase.EndDate) : phase.EndDate;

            var building = _db.Buildings.Get(phase.BuildingId);
            building.StartDate = startDate;
            building.EndDate = endDate;
            building.FullPrice = phases.Sum(p => p.FullPrice) + phase.FullPrice;
            building.ExtraPrice = phases.Sum(p => p.ExtraPrice) + phase.ExtraPrice;
            building.DurationInDays = (building.EndDate - building.StartDate)?.Days;

            _db.Buildings.Update(building);
        }

        private static DateTime? Min(DateTime? date1, DateTime? date2) => date1 > date2 ? date2 : date1;
        private static DateTime? Max(DateTime? date1, DateTime? date2) => date1 > date2 ? date1 : date2;
    }
}