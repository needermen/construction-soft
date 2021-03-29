using Cs.Domain.Buildings;

namespace Cs.Application.Interfaces
{
    public interface IBuildingDbService : IDbService
    {
        IRepository<Building> Buildings { get; set; }
        IRepository<Phase> Phases { get; set; }
        IRepository<WorkCategory> WorkCategories { get; set; }
        IRepository<Work> Works { get; set; }
    }
}