using System;
using System.Linq;
using AutoMapper;
using Cs.Application.Buildings.Models;
using Cs.Application.Files.Models;
using Cs.Application.Hr.Models;
using Cs.Application.Org.Models;
using Cs.Domain.Auth;
using Cs.Domain.Buildings;
using Cs.Domain.Files;
using Cs.Domain.Hr;
using WorkMainMaterial = Cs.Domain.Buildings.WorkMainMaterial;

namespace Cs.Application.Tools.Mapper
{
    public class DomainToViewProfile : Profile
    {
        public DomainToViewProfile()
        {
            //hr

            CreateMap<User, UserViewModel>()
                .ForMember(dest => dest.RoleIds, op => op.MapFrom(src => src.Roles.Select(r => r.RoleId).ToArray()))
                .ForMember(dest => dest.OrganizationName,
                    op => op.MapFrom(src => src.Organization != null ? src.Organization.Name : null));

            CreateMap<Organization, OrganizationViewModel>()
                .ForMember(dest => dest.RoleIds, op => op.MapFrom(src => src.Roles.Select(r => r.RoleId).ToArray()))
                .ForMember(dest => dest.Logo,
                    op => op.MapFrom(src =>
                        src.Logo == null || src.Logo.Length == 0 ? null : Convert.ToBase64String(src.Logo)));

            CreateMap<Brigade, BrigadeViewModel>()
                .ForMember(b => b.CategoryName, op => op.MapFrom(src => src.Category.Name))
                .ForMember(b => b.PaymentTypeName, op => op.MapFrom(src => src.PaymentType.Name));

            CreateMap<BrigadeFile, FileViewModel>()
                .ForMember(f => f.Id, op => op.MapFrom(src => src.FileId))
                .ForMember(f => f.FileName, op => op.MapFrom(src => src.File.Filename));

            CreateMap<Worker, WorkerViewModel>()
                .ForMember(w => w.CategoryName, op => op.MapFrom(src => src.Category.Name))
                .ForMember(w => w.PaymentTypeName, op => op.MapFrom(src => src.PaymentType.Name));

            CreateMap<WorkerFile, FileViewModel>()
                .ForMember(wf => wf.Id, op => op.MapFrom(src => src.FileId))
                .ForMember(wf => wf.FileName, op => op.MapFrom(src => src.File.Filename));

            //building

            CreateMap<Building, BuildingViewModel>()
                .ForMember(dest => dest.StatusName, op => op.MapFrom(src => src.Status.ToString("G")));

            CreateMap<Phase, PhaseViewModel>();
            CreateMap<WorkCategory, WorkCategoryViewModel>();
            CreateMap<Work, WorkViewModel>()
                .ForMember(dest => dest.HasToBeDoneAfterName,
                    op => op.MapFrom(src => src.HasToBeDoneAfter == null ? null : src.HasToBeDoneAfter.Name));


            //building -> work

            CreateMap<WorkBuildingMaterial, WorkBuildingMaterialViewModel>()
                .ForMember(dest => dest.MaterialName, op =>
                    op.MapFrom(src => src.BuildingMaterial.Name))
                .ForMember(dest => dest.MaterialPrice, op =>
                    op.MapFrom(src => src.BuildingMaterial.Price))
                .ForMember(dest => dest.MaterialCategory, op =>
                    op.MapFrom(src => src.BuildingMaterial.Category.Name))
                .ForMember(dest => dest.MaterialCoefficient, op =>
                    op.MapFrom(src => src.BuildingMaterial.Coefficient))
                .ForMember(dest => dest.MaterialDimension, op =>
                    op.MapFrom(src => src.BuildingMaterial.Dimension.Name));

            CreateMap<WorkConsumptionMaterial, WorkConsumptionMaterialViewModel>()
                .ForMember(dest => dest.MaterialName, op =>
                    op.MapFrom(src => src.Material.Name))
                .ForMember(dest => dest.MaterialPrice, op =>
                    op.MapFrom(src => src.Material.Price))
                .ForMember(dest => dest.MaterialCoefficient, op =>
                    op.MapFrom(src => src.Material.Coefficient))
                .ForMember(dest => dest.MaterialCategory, op =>
                    op.MapFrom(src => src.Material.Category.Name))
                .ForMember(dest => dest.MaterialDimension, op =>
                    op.MapFrom(src => src.Material.Dimension.Name));

            CreateMap<WorkMainMaterial, WorkMainMaterialViewModel>()
                .ForMember(dest => dest.MaterialName, op =>
                    op.MapFrom(src => src.Material.Name))
                .ForMember(dest => dest.MaterialPrice, op =>
                    op.MapFrom(src => src.Material.Price))
                .ForMember(dest => dest.MaterialCoefficient, op =>
                    op.MapFrom(src => src.Material.Coefficient))
                .ForMember(dest => dest.MaterialDepreciation, op =>
                    op.MapFrom(src => src.Material.Depreciation))
                .ForMember(dest => dest.MaterialCategory, op =>
                    op.MapFrom(src => src.Material.Category.Name))
                .ForMember(dest => dest.MaterialDimension, op =>
                    op.MapFrom(src => src.Material.Dimension.Name));

            CreateMap<WorkTechnic, WorkTechnicViewModel>()
                .ForMember(dest => dest.TechnicName, op =>
                    op.MapFrom(src => src.Technic.Name))
                .ForMember(dest => dest.TechnicPrice, op =>
                    op.MapFrom(src => src.Technic.Price))
                .ForMember(dest => dest.TechnicCoefficient, op =>
                    op.MapFrom(src => src.Technic.Coefficient))
                .ForMember(dest => dest.TechnicCategory, op =>
                    op.MapFrom(src => src.Technic.Category.Name))
                .ForMember(dest => dest.TechnicDimension, op =>
                    op.MapFrom(src => src.Technic.Dimension.Name));

            CreateMap<WorkWorker, WorkWorkerViewModel>()
                .ForMember(dest => dest.WorkerName, op =>
                    op.MapFrom(src => src.Worker.Name))
                .ForMember(dest => dest.WorkerSalary, op =>
                    op.MapFrom(src => src.Worker.Salary))
                .ForMember(dest => dest.WorkerCoefficient, op =>
                    op.MapFrom(src => src.Worker.Coefficient))
                .ForMember(dest => dest.WorkerCategory, op =>
                    op.MapFrom(src => src.Worker.Category.Name))
                .ForMember(dest => dest.WorkerPaymentType, op =>
                    op.MapFrom(src => src.Worker.PaymentType.Name));

            CreateMap<WorkBrigade, WorkBrigadeViewModel>()
                .ForMember(dest => dest.BrigadeName, op =>
                    op.MapFrom(src => src.Brigade.Name))
                .ForMember(dest => dest.BrigadeSalary, op =>
                    op.MapFrom(src => src.Brigade.Salary))
                .ForMember(dest => dest.BrigadeCategory, op =>
                    op.MapFrom(src => src.Brigade.Category.Name))
                .ForMember(dest => dest.BrigadePaymentType, op =>
                    op.MapFrom(src => src.Brigade.PaymentType.Name));
        }
    }
}