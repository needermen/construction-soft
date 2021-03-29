using System;
using System.Linq;
using AutoMapper;
using Cs.Application.Buildings.Models;
using Cs.Application.Hr.Models;
using Cs.Application.Org.Models;
using Cs.Domain.Auth;
using Cs.Domain.Buildings;
using Cs.Domain.Files;
using Cs.Domain.Hr;

namespace Cs.Application.Tools.Mapper
{
    public class ViewToDomainProfile : Profile
    {
        public ViewToDomainProfile()
        {
            //hr

            CreateMap<UserViewModel, User>()
                .ForMember(dest => dest.Roles, op => op.MapFrom(src => src.RoleIds.Select(roleId => new UserRole
                {
                    RoleId = roleId
                })));

            CreateMap<OrganizationViewModel, Organization>()
                .ForMember(dest => dest.Roles, op => op.MapFrom(src => src.RoleIds.Select(roleId =>
                    new OrganizationRole()
                    {
                        RoleId = roleId
                    })))
                .ForMember(dest => dest.Logo,
                    op => op.MapFrom(src =>
                        string.IsNullOrEmpty(src.Logo) ? null : Convert.FromBase64String(src.Logo)));

            CreateMap<BrigadeViewModel, Brigade>()
                .ForMember(dest => dest.Files,
                    op => op.MapFrom(src => src.Files.Select(f => new BrigadeFile {FileId = f.Id})));

            CreateMap<WorkerViewModel, Worker>()
                .ForMember(dest => dest.Files,
                    op => op.MapFrom(src => src.Files.Select(f => new WorkerFile {FileId = f.Id})));

            //building

            CreateMap<BuildingViewModel, Building>();
            CreateMap<PhaseViewModel, Phase>();
            CreateMap<WorkCategoryViewModel, WorkCategory>();
            CreateMap<WorkViewModel, Work>();

            //building -> work

            CreateMap<WorkBuildingMaterialViewModel, WorkBuildingMaterial>();
            CreateMap<WorkConsumptionMaterialViewModel, WorkConsumptionMaterial>();
            CreateMap<WorkMainMaterialViewModel, WorkMainMaterial>();
            CreateMap<WorkTechnicViewModel, WorkTechnic>();
            CreateMap<WorkWorkerViewModel, WorkWorker>();
            CreateMap<WorkBrigadeViewModel, WorkBrigade>();
        }
    }
}