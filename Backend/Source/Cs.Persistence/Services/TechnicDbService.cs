using System.Transactions;
using Cs.Application.Interfaces;
using Cs.Domain.Technics;

namespace Cs.Persistence.Services
{
    public class TechnicDbService : ITechnicDbService
    {
        public IRepository<Technic> Technics { get; set; }
        public IRepository<TechnicCategory> TechnicCategories { get; set; }
        public IRepository<TechnicDimension> TechnicDimensions { get; set; }

        public TechnicDbService(IRepository<Technic> technics,
            IRepository<TechnicCategory> technicCategories,
            IRepository<TechnicDimension> technicDimensions)
        {
            Technics = technics;
            TechnicCategories = technicCategories;
            TechnicDimensions = technicDimensions;
        }

        public void Save()
        {
            using (var scope = new TransactionScope())
            {
                if (TechnicCategories.Changed())
                    TechnicDimensions.Save();
                
                if (TechnicDimensions.Changed())
                    TechnicDimensions.Save();
                
                if (Technics.Changed())
                    Technics.Save();

                scope.Complete();
            }
        }
    }
}