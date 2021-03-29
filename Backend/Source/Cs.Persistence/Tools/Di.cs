using Cs.Application.Interfaces;
using Cs.Persistence.Services;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;

namespace Cs.Persistence.Tools
{
    public static class Di
    {
        public static IServiceCollection AddPersistenceServices(this IServiceCollection services,
            string connectionString)
        {
            services.AddDbContext<CsDbContext>(opt => { opt.UseSqlServer(connectionString); });

            services.AddScoped<DbContext, CsDbContext>();
            services.AddScoped(typeof(IRepository<>), typeof(Repository<>));

            services.AddScoped<IAuthDbService, AuthDbService>();
            services.AddScoped<ITechnicDbService, TechnicDbService>();
            services.AddScoped<IMaterialDbService, MaterialDbService>();
            services.AddScoped<IHrDbService, HrDbService>();
            services.AddScoped<IBuildingDbService, BuildingDbService>();
            services.AddScoped<IStorageDbService, StorageDbService>();

            return services;
        }
    }
}