using Cs.Domain.Materials;

namespace Cs.Application.Interfaces
{
    public interface IMaterialDbService : IDbService
    {
        IRepository<BuildingMaterial> BuildingMaterials { get; set; }
        IRepository<BuildingMaterialCategory> BuildingMaterialCategories { get; set; }
        IRepository<ConsumptionMaterial> ConsumptionMaterials { get; set; }
        IRepository<ConsumptionMaterialCategory> ConsumptionMaterialCategories { get; set; }
        IRepository<MainMaterial> MainMaterials { get; set; }
        IRepository<MainMaterialCategory> MainMaterialCategories { get; set;  }
        IRepository<MaterialDimension> MaterialDimensions { get; set; }
    }
}