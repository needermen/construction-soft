using Cs.Domain.Technics;

namespace Cs.Application.Interfaces
{
    public interface ITechnicDbService : IDbService
    {
        IRepository<Domain.Technics.Technic> Technics { get; set; }
        IRepository<TechnicCategory> TechnicCategories { get; set; }
        IRepository<TechnicDimension> TechnicDimensions { get; set; }
    }
}