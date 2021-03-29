using Cs.Application.Auth;
using Cs.Application.Buildings;
using Cs.Application.Buildings.Models;
using Cs.Application.Files;
using Cs.Application.Hr;
using Cs.Application.Hr.Models;
using Cs.Application.Interfaces;
using Cs.Application.Material;
using Cs.Application.Org;
using Cs.Application.Org.Models;
using Cs.Application.Technic;
using Cs.Domain.Hr;
using Cs.Domain.Materials;
using Cs.Domain.Technics;
using Microsoft.Extensions.DependencyInjection;

namespace Cs.Application.Tools
{
    public static class Di
    {
        public static IServiceCollection AddApplicationServices(this IServiceCollection services)
        {
            //Auth
            services.AddScoped<IAuthService, AuthService>();
            
            //Technic
            services.AddScoped<ICrudOperation<TechnicCategory>, TechnicCategoryService>();
            services.AddScoped<ICrudOperation<TechnicDimension>, TechnicDimensionService>();
            services.AddScoped<ICrudOperation<Domain.Technics.Technic>, TechnicService>();

            //Material
            services.AddScoped<ICrudOperation<MaterialDimension>, MaterialDimensionService>();
            services.AddScoped<ICrudOperation<BuildingMaterial>, BuildingMaterialService>();
            services.AddScoped<ICrudOperation<BuildingMaterialCategory>, BuildingMaterialCategoryService>();
            services.AddScoped<ICrudOperation<ConsumptionMaterial>, ConsumptionMaterialService>();
            services.AddScoped<ICrudOperation<ConsumptionMaterialCategory>, ConsumptionMaterialCategoryService>();
            services.AddScoped<ICrudOperation<MainMaterial>, MainMaterialService>();
            services.AddScoped<ICrudOperation<MainMaterialCategory>, MainMaterialCategoryService>();
            
            //Hr
            services.AddScoped<ICrudOperation<PaymentType>, PaymentTypeService>();
            services.AddScoped<ICrudOperation<WorkerViewModel>, WorkerService>();
            services.AddScoped<ICrudOperation<WorkerCategory>, WorkerCategoryService>();
            services.AddScoped<ICrudOperation<BrigadeViewModel>, BrigadeService>();
            services.AddScoped<ICrudOperation<BrigadeCategory>, BrigadeCategoryService>();
            
            //Org
            services.AddScoped<ICrudOperation<OrganizationViewModel>, OrganizationService>();
            services.AddScoped<IUserService, UserService>();
            
            //Building
            services.AddScoped<IBuildingService, BuildingService>();
            services.AddScoped<IPhaseService, PhaseService>();
            services.AddScoped<IWorkCategoryService, WorkCategoryService>();
            services.AddScoped<IWorkService, WorkService>();
            
            //Building -> Work
            services.AddScoped<IDependedCrudOperation<WorkBuildingMaterialViewModel>, WorkBuildingMaterialService>();
            services.AddScoped<IDependedCrudOperation<WorkConsumptionMaterialViewModel>, WorkConsumptionMaterialService>();
            services.AddScoped<IDependedCrudOperation<WorkMainMaterialViewModel>, WorkMainMaterialService>();
            services.AddScoped<IDependedCrudOperation<WorkTechnicViewModel>, WorkTechnicService>();
            services.AddScoped<IDependedCrudOperation<WorkWorkerViewModel>, WorkWorkerService>();
            services.AddScoped<IDependedCrudOperation<WorkBrigadeViewModel>, WorkBrigadeService>();
            
            //Files
            services.AddScoped<IFileService, FileService>();
            

            return services;
        }
    }
}