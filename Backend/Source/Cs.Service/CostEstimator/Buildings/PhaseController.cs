using Cs.Application.Buildings;
using Cs.Application.Buildings.Models;
using Cs.Application.Tools;
using Cs.Common.Models;
using Cs.Service.Common.Models;
using Microsoft.AspNetCore.Mvc;

namespace Cs.Service.CostEstimator.Buildings
{
    public class PhaseController : CostEstimatorController
    {
        private readonly IPhaseService _phases;
        private readonly IWorkCategoryService _workCategories;

        public PhaseController(IPhaseService phases, IWorkCategoryService workCategories)
        {
            _phases = phases;
            _workCategories = workCategories;
        }

        [HttpGet]
        public IActionResult Get([FromQuery] Paging paging)
        {
            var result = _phases.Get(paging, ValidateAndGetOrgId());

            return Json(ServiceResult<ListResult<PhaseViewModel>>.Ok(result));
        }

        [HttpGet("{id}")]
        public IActionResult Get(int id)
        {
            var phase = _validateForOrgIdAndGet(id);

            return Json(ServiceResult<PhaseViewModel>.Ok(phase));
        }

        [HttpGet("{id}/dates")]
        public IActionResult GetDates(int id)
        {
            _validateForOrgIdAndGet(id);

            var dates = _phases.GetDateRanges(id);

            return Json(ServiceResult<ListResult<DateRangeViewModel>>.Ok(dates));
        }

        [HttpPost]
        public IActionResult Post([FromBody] PhaseViewModel phase)
        {
            phase.OrganizationId = ValidateAndGetOrgId();

            var id = _phases.Add(phase);

            return Json(ServiceResult<int>.Ok(id));
        }

        [HttpPut("{id}")]
        public IActionResult Put(int id, [FromBody] PhaseViewModel phase)
        {
            _validateForOrgIdAndGet(phase.Id);

            _phases.Update(phase);

            return Json(ServiceResult<bool>.Ok(true));
        }

        [HttpDelete("{id}")]
        public IActionResult Delete(int id)
        {
            _validateForOrgIdAndGet(id);

            _phases.Delete(id);

            return Json(ServiceResult<bool>.Ok(true));
        }

        [Route("{id}/WorkCategory")]
        [HttpGet]
        public IActionResult GetWorkCategories(int id)
        {
            _validateForOrgIdAndGet(id);

            var result = _workCategories.GetByPhase(id);

            return Json(ServiceResult<ListResult<WorkCategoryViewModel>>.Ok(result));
        }

        private PhaseViewModel _validateForOrgIdAndGet(int id)
        {
            var phase = _phases.Get(id);
            if (phase == null)
                throw new ServiceException(UiConstants.ObjectNotFound);
            if (phase.OrganizationId != GetOrgId())
                throw new ServiceException(UiConstants.UnauthorizedAccess);
            return phase;
        }
    }
}