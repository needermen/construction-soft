using Cs.Application.Buildings.Models;
using Cs.Application.Interfaces;
using Cs.Common.Models;

namespace Cs.Application.Buildings
{
    public interface IWorkCategoryService : ICrudOperation<WorkCategoryViewModel>, IGetDateRangesService
    {
        ListResult<WorkCategoryViewModel> GetByPhase(int phaseId);
        ListResult<WorkCategoryViewModel> Get(Paging paging, int orgId);
    }
}