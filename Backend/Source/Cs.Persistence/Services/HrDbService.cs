using System.Transactions;
using Cs.Application.Interfaces;
using Cs.Domain.Hr;

namespace Cs.Persistence.Services
{
    public class HrDbService : IHrDbService
    {
        public IRepository<Worker> Workers { get; set; }
        public IRepository<WorkerCategory> WorkerCategories { get; set; }
        public IRepository<Brigade> Brigades { get; set; }
        public IRepository<BrigadeCategory> BrigadeCategories { get; set; }
        public IRepository<PaymentType> PaymentTypes { get; set; }

        public HrDbService(IRepository<Worker> workers, IRepository<WorkerCategory> workerCategories, IRepository<Brigade> brigades, IRepository<BrigadeCategory> brigadeCategories, IRepository<PaymentType> paymentTypes)
        {
            Workers = workers;
            WorkerCategories = workerCategories;
            Brigades = brigades;
            BrigadeCategories = brigadeCategories;
            PaymentTypes = paymentTypes;
        }
        
        public void Save()
        {
            using (var scope = new TransactionScope())
            {
                if(Workers.Changed())
                    Workers.Save();
                
                if(WorkerCategories.Changed())
                    WorkerCategories.Save();
                
                if(Brigades.Changed())
                    Brigades.Save();
                
                if(BrigadeCategories.Changed())
                    BrigadeCategories.Save();
                
                if(PaymentTypes.Changed())
                    PaymentTypes.Save();
                
                scope.Complete();
            }
        }
    }
}