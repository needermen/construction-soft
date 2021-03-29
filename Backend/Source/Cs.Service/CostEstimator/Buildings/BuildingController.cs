using Cs.Application.Buildings;
using Cs.Application.Buildings.Models;
using Cs.Application.Tools;
using Cs.Common.Models;
using Cs.Service.Common.Models;
using Microsoft.AspNetCore.Mvc;

namespace Cs.Service.CostEstimator.Buildings
{
    public class BuildingController : CostEstimatorController
    {
        private readonly IBuildingService _buildings;
        private readonly IPhaseService _phases;

        public BuildingController(IBuildingService buildings, IPhaseService phases)
        {
            _buildings = buildings;
            _phases = phases;
        }

        [HttpGet]
        public IActionResult Get([FromQuery] Paging paging)
        {
            var result = _buildings.Get(paging, ValidateAndGetOrgId());

            return Json(ServiceResult<ListResult<BuildingViewModel>>.Ok(result));
        }

        [HttpGet("{id}")]
        public IActionResult Get(int id)
        {
            var building = _validateForOrgIdAndGet(id);

            return Json(ServiceResult<BuildingViewModel>.Ok(building));
        }

        [HttpGet("{id}/dates")]
        public IActionResult GetDates(int id)
        {
            _validateForOrgIdAndGet(id);

            var dates = _buildings.GetDateRanges(id);

            return Json(ServiceResult<ListResult<DateRangeViewModel>>.Ok(dates));
        }

        [HttpPost]
        public IActionResult Post([FromBody] BuildingViewModel building)
        {
            building.OrganizationId = ValidateAndGetOrgId();

            var id = _buildings.Add(building);

            return Json(ServiceResult<int>.Ok(id));
        }

        [HttpPut("{id}")]
        public IActionResult Put(int id, [FromBody] BuildingViewModel building)
        {
            _validateForOrgIdAndGet(building.Id);

            _buildings.Update(building);

            return Json(ServiceResult<bool>.Ok(true));
        }

        [HttpDelete("{id}")]
        public IActionResult Delete(int id)
        {
            _validateForOrgIdAndGet(id);

            _buildings.Delete(id);

            return Json(ServiceResult<bool>.Ok(true));
        }

        [Route("{id}/Phase")]
        [HttpGet]
        public IActionResult GetPhases(int id)
        {
            _validateForOrgIdAndGet(id);

            var result = _phases.GetByBuilding(id);

            return Json(ServiceResult<ListResult<PhaseViewModel>>.Ok(result));
        }

        private BuildingViewModel _validateForOrgIdAndGet(int id)
        {
            var building = _buildings.Get(id);

            if (building == null)
                throw new ServiceException(UiConstants.ObjectNotFound);
            if (building.OrganizationId != GetOrgId())
                throw new ServiceException(UiConstants.UnauthorizedAccess);

            return building;
        }
    }
}