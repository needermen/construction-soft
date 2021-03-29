using Cs.Domain.Hr;

namespace Cs.Application.Interfaces
{
    public interface IHrDbService : IDbService
    {
        IRepository<Worker> Workers { get; set; }
        IRepository<WorkerCategory> WorkerCategories { get; set; }
        IRepository<Brigade> Brigades { get; set; }
        IRepository<BrigadeCategory> BrigadeCategories { get; set; }
        IRepository<PaymentType> PaymentTypes { get; set; }
    }
}