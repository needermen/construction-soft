using Cs.Application.Buildings;
using Cs.Application.Buildings.Models;
using Cs.Application.Tools;
using Cs.Common.Models;
using Cs.Service.Common.Models;
using Microsoft.AspNetCore.Mvc;

namespace Cs.Service.CostEstimator.Buildings
{
    public class WorkController : CostEstimatorController
    {
        private readonly IWorkService _works;

        public WorkController(IWorkService works)
        {
            _works = works;
        }

        [HttpGet]
        public IActionResult Get([FromQuery] Paging paging)
        {
            var result = _works.Get(paging, ValidateAndGetOrgId());

            return Json(ServiceResult<ListResult<WorkViewModel>>.Ok(result));
        }

        [HttpGet("{id}")]
        public IActionResult Get(int id)
        {
            var work = _validateForOrgIdAndGet(id);

            return Json(ServiceResult<WorkViewModel>.Ok(work));
        }

        [HttpGet("{id}/dates")]
        public IActionResult GetDates(int id)
        {
            _validateForOrgIdAndGet(id);
            
            var dates = _works.GetDateRanges(id);

            return Json(ServiceResult<ListResult<DateRangeViewModel>>.Ok(dates));
        }

        [HttpPost]
        public IActionResult Post([FromBody] WorkViewModel work)
        {
            work.OrganizationId = ValidateAndGetOrgId();
            
            var id = _works.Add(work);

            return Json(ServiceResult<int>.Ok(id));
        }

        [HttpPut("{id}")]
        public IActionResult Put(int id, [FromBody] WorkViewModel work)
        {
            _validateForOrgIdAndGet(work.Id);
            
            _works.Update(work);

            return Json(ServiceResult<bool>.Ok(true));
        }

        [HttpDelete("{id}")]
        public IActionResult Delete(int id)
        {
            _validateForOrgIdAndGet(id);
            
            _works.Delete(id);

            return Json(ServiceResult<bool>.Ok(true));
        }
          
        private WorkViewModel _validateForOrgIdAndGet(int id)
        {
            var work = _works.Get(id);
            if (work == null)
                throw new ServiceException(UiConstants.ObjectNotFound);
            if (work.OrganizationId != GetOrgId())
                throw new ServiceException(UiConstants.UnauthorizedAccess);
            return work;
        }
    }
}