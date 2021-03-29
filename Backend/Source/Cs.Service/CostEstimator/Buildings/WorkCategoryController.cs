using Cs.Application.Buildings;
using Cs.Application.Buildings.Models;
using Cs.Application.Tools;
using Cs.Common.Models;
using Cs.Service.Common.Models;
using Microsoft.AspNetCore.Mvc;

namespace Cs.Service.CostEstimator.Buildings
{
    public class WorkCategoryController : CostEstimatorController
    {
        private readonly IWorkCategoryService _workCategories;
        private readonly IWorkService _works;

        public WorkCategoryController(IWorkCategoryService workCategories, IWorkService works)
        {
            _workCategories = workCategories;
            _works = works;
        }

        [HttpGet]
        public IActionResult Get([FromQuery] Paging paging)
        {
            var result = _workCategories.Get(paging, ValidateAndGetOrgId());
            
            return Json(ServiceResult<ListResult<WorkCategoryViewModel>>.Ok(result));
        }

        [HttpGet("{id}")]
        public IActionResult Get(int id)
        {
            var workCategory = _validateForOrgIdAndGet(id);

            return Json(ServiceResult<WorkCategoryViewModel>.Ok(workCategory));
        }
        
        [HttpGet("{id}/dates")]
        public IActionResult GetDates(int id)
        {    
            _validateForOrgIdAndGet(id);
            
            var dates = _workCategories.GetDateRanges(id);

            return Json(ServiceResult<ListResult<DateRangeViewModel>>.Ok(dates));
        }

        [HttpPost]
        public IActionResult Post([FromBody]WorkCategoryViewModel workCategory)
        {
            workCategory.OrganizationId = ValidateAndGetOrgId();
            
            var id = _workCategories.Add(workCategory);

            return Json(ServiceResult<int>.Ok(id));
        }

        [HttpPut("{id}")]
        public IActionResult Put(int id, [FromBody]WorkCategoryViewModel workCategory)
        {
            _validateForOrgIdAndGet(workCategory.Id);
            
            _workCategories.Update(workCategory);

            return Json(ServiceResult<bool>.Ok(true));
        }

        [HttpDelete("{id}")]
        public IActionResult Delete(int id)
        {
            _validateForOrgIdAndGet(id);
            
            _workCategories.Delete(id);

            return Json(ServiceResult<bool>.Ok(true));
        }
        
        [Route("{id}/Work")]
        [HttpGet]
        public IActionResult GetWorks(int id)
        {
            _validateForOrgIdAndGet(id);
            
            var result = _works.GetByWorkCategory(id);
            
            return Json(ServiceResult<ListResult<WorkViewModel>>.Ok(result));
        }
        
        private WorkCategoryViewModel _validateForOrgIdAndGet(int id)
        {
            var workCategory = _workCategories.Get(id);
            if (workCategory == null)
                throw new ServiceException(UiConstants.ObjectNotFound);
            if (workCategory.OrganizationId != GetOrgId())
                throw new ServiceException(UiConstants.UnauthorizedAccess);
            return workCategory;
        }
    }
}