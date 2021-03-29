using Cs.Domain.Auth;
using Cs.Service.Common;
using Cs.Service.Configuration.Filters;

namespace Cs.Service.CostEstimator
{
    [AuthFilter(new[] { RoleEnums.CostEstimator})]
    public class CostEstimatorController : BaseController
    {
    }
}