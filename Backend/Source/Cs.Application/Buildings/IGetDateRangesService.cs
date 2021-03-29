using Cs.Application.Buildings.Models;
using Cs.Common.Models;

namespace Cs.Application.Buildings
{
    public interface IGetDateRangesService
    {
        ListResult<DateRangeViewModel> GetDateRanges(int id);
    }
}