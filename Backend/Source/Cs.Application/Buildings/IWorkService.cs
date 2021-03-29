using Cs.Application.Buildings.Models;
using Cs.Application.Interfaces;
using Cs.Common.Models;

namespace Cs.Application.Buildings
{
    public interface IWorkService : ICrudOperation<WorkViewModel>, IGetDateRangesService
    {
        ListResult<WorkViewModel> GetByWorkCategory(int workCategoryId);
        ListResult<WorkViewModel> Get(Paging paging, int orgId);
    }
}