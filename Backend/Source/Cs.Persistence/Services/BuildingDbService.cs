using System.Transactions;
using Cs.Application.Interfaces;
using Cs.Domain.Buildings;

namespace Cs.Persistence.Services
{
    public class BuildingDbService : IBuildingDbService
    {
        public IRepository<Building> Buildings { get; set; }
        public IRepository<Phase> Phases { get; set; }
        public IRepository<WorkCategory> WorkCategories { get; set; }
        public IRepository<Work> Works { get; set; }

        public BuildingDbService(IRepository<Building> buildings, IRepository<Phase> phases, 
            IRepository<WorkCategory> workCategories, IRepository<Work> works)
        {
            Buildings = buildings;
            Phases = phases;
            WorkCategories = workCategories;
            Works = works;
         }
        
        public void Save()
        {
            using (var scope = new TransactionScope())
            {
                if(Buildings.Changed())
                    Buildings.Save();
                
                if(Phases.Changed())
                    Phases.Save();
                
                if(WorkCategories.Changed())
                    WorkCategories.Save();
                
                if(Works.Changed())
                    Works.Save();
                
                scope.Complete();
            }
        }
    }
}