using System.Transactions;
using Cs.Application.Interfaces;
using Cs.Domain.Materials;

namespace Cs.Persistence.Services
{
    public class MaterialDbService : IMaterialDbService
    {
        public IRepository<BuildingMaterial> BuildingMaterials { get; set; }
        public IRepository<BuildingMaterialCategory> BuildingMaterialCategories { get; set; }
        public IRepository<ConsumptionMaterial> ConsumptionMaterials { get; set; }
        public IRepository<ConsumptionMaterialCategory> ConsumptionMaterialCategories { get; set; }
        public IRepository<MainMaterial> MainMaterials { get; set; }
        public IRepository<MainMaterialCategory> MainMaterialCategories { get; set; }
        public IRepository<MaterialDimension> MaterialDimensions { get; set; }
        
        public MaterialDbService(IRepository<BuildingMaterial> buildingMaterials, IRepository<BuildingMaterialCategory> buildingMaterialCategories, IRepository<ConsumptionMaterial> consumptionMaterials, IRepository<ConsumptionMaterialCategory> consumptionMaterialCategories, IRepository<MainMaterial> mainMaterials, IRepository<MainMaterialCategory> mainMaterialCategories, IRepository<MaterialDimension> materialDimensions)
        {
            BuildingMaterials = buildingMaterials;
            BuildingMaterialCategories = buildingMaterialCategories;
            ConsumptionMaterials = consumptionMaterials;
            ConsumptionMaterialCategories = consumptionMaterialCategories;
            MainMaterials = mainMaterials;
            MainMaterialCategories = mainMaterialCategories;
            MaterialDimensions = materialDimensions;
        }
        
        public void Save()
        {
            using (var scope = new TransactionScope())
            {
                if(BuildingMaterials.Changed())
                    BuildingMaterials.Save();
                
                if(BuildingMaterialCategories.Changed())
                    BuildingMaterialCategories.Save();
                
                if(ConsumptionMaterials.Changed())
                    ConsumptionMaterials.Save();
                
                if(ConsumptionMaterialCategories.Changed())
                    ConsumptionMaterialCategories.Save();
                
                if(MainMaterials.Changed())
                    MainMaterials.Save();
                
                if(MainMaterialCategories.Changed())
                    MainMaterialCategories.Save();
                
                if(MaterialDimensions.Changed())
                    MaterialDimensions.Save();
                
                scope.Complete();
            }
        }

    }
}