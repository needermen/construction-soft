using Cs.Application.Buildings.Models;
using Cs.Application.Interfaces;
using Cs.Common.Models;

namespace Cs.Application.Buildings
{
    public interface IBuildingService : ICrudOperation<BuildingViewModel>, IGetDateRangesService
    {
        ListResult<BuildingViewModel> Get(Paging paging, int organizationId);
    }
}