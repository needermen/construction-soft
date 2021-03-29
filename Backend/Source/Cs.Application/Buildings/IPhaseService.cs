using Cs.Application.Buildings.Models;
using Cs.Application.Interfaces;
using Cs.Common.Models;

namespace Cs.Application.Buildings
{
    public interface IPhaseService : ICrudOperation<PhaseViewModel>, IGetDateRangesService
    {
        ListResult<PhaseViewModel> GetByBuilding(int buildingId);
        ListResult<PhaseViewModel> Get(Paging paging, int orgId);
    }
}