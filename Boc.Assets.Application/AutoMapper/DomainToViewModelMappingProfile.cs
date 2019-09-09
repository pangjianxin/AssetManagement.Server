using AutoMapper;
using Boc.Assets.Application.Dto;
using Boc.Assets.Domain.Models.Assets;
using Boc.Assets.Domain.Models.Assets.Audit;
using Boc.Assets.Domain.Models.AssetStockTakings;
using Boc.Assets.Domain.Models.Organizations;

namespace Boc.Assets.Application.AutoMapper
{
    public class DomainToViewModelMappingProfile : Profile
    {
        public DomainToViewModelMappingProfile()
        {
            CreateMap<OrganizationRole, OrganizationRoleDto>()
                .ForMember(it => it.RoleNam, config => config.MapFrom(it => it.Role.ToString()))
                .ForMember(it => it.RoleEnum, config => config.MapFrom(it => (int)it.Role));
            //机构和机构DTO之间的映射
            CreateMap<Organization, OrgDto>()
                .ForMember(it => it.Role, config => config.MapFrom(it => (int)it.Role.Role))
                .ForMember(it => it.RoleDescription, config => config.MapFrom(it => it.Role.Description))
                .ForMember(it => it.OrgId, config => config.MapFrom(it => it.Id));

            //资产分类和DTO之间的映射
            CreateMap<AssetCategory, AssetCategoryDto>()
                .ForMember(it => it.AssetMeteringUnit, config => config.MapFrom(it => it.AssetMeteringUnit.ToString()))
                .ForMember(it => it.AssetCategoryId, config => config.MapFrom(it => it.Id));
            //资产和DTO之间的映射
            CreateMap<Asset, AssetDto>()
                .ForMember(it => it.AssetId, config => config.MapFrom(it => it.Id))
                .ForMember(it => it.AssetCategoryDto, config => config.MapFrom(it => it.AssetCategory));
            //机构空间和DTO之间的映射
            CreateMap<OrganizationSpace, OrgSpaceDto>()
                .ForMember(it => it.SpaceId, config => config.MapFrom(it => it.Id));
            //资产申请事件和DTO之间的映射
            CreateMap<AssetApply, AssetApplyDto>()
                .ForMember(it => it.Status, config => config.MapFrom(it => it.Status.ToString()))
                .ForMember(it => it.EventId, config => config.MapFrom(it => it.Id))
                .ForMember(it => it.DateTimeFromNow, config => config.MapFrom(it => it.DateTimeFromNow()));
            //资产交回事件和DTO之间的映射
            CreateMap<AssetReturn, AssetReturnDto>()
                .ForMember(it => it.Status, config => config.MapFrom(it => it.Status.ToString()))
                .ForMember(it => it.EventId, config => config.MapFrom(it => it.Id))
                .ForMember(it => it.DateTimeFromNow, config => config.MapFrom(it => it.DateTimeFromNow()));
            //资产机构间调换和DTO之间的映射
            CreateMap<AssetExchange, AssetExchangeDto>()
                .ForMember(it => it.Status, config => config.MapFrom(it => it.Status.ToString()))
                .ForMember(it => it.EventId, config => config.MapFrom(it => it.Id))
                .ForMember(it => it.DateTimeFromNow, config => config.MapFrom(it => it.DateTimeFromNow()));
            //资产调配记录和DTO之间的映射
            CreateMap<AssetDeploy, AssetDeployDto>()
                .ForMember(it => it.AssetDeployCategory,
                    config => config.MapFrom(it => it.AssetDeployCategory.ToString()))
                .ForMember(it => it.ExportOrgIdentifier, config => config.MapFrom(it => it.ExportOrgInfo.OrgIdentifier))
                .ForMember(it => it.ExportOrgNam, config => config.MapFrom(it => it.ExportOrgInfo.OrgNam))
                .ForMember(it => it.ImportOrgIdentifier, config => config.MapFrom(it => it.ImportOrgInfo.OrgIdentifier))
                .ForMember(it => it.ImportOrgNam, config => config.MapFrom(it => it.ImportOrgInfo.OrgNam))
                .ForMember(it => it.AuthorizeOrgIdentifier,
                    config => config.MapFrom(it => it.AuthorizeOrgInfo.OrgIdentifier))
                .ForMember(it => it.AuthorizeOrgNam, config => config.MapFrom(it => it.AuthorizeOrgInfo.OrgNam));
            //资产盘点参与机构和DTO之间的映射
            CreateMap<AssetStockTakingOrganization, AssetStockTakingOrgDto>()
                .ForMember(it => it.OrgId, config => config.MapFrom(it => it.OrganizationId))
                .ForMember(it => it.AssetStockTakingId, config => config.MapFrom(it => it.AssetStockTakingId))
                .ForMember(it => it.OrgIdentifier, config => config.MapFrom(it => it.Organization.OrgIdentifier))
                .ForMember(it => it.OrgNam, config => config.MapFrom(it => it.Organization.OrgNam))
                .ForMember(it => it.Org2, config => config.MapFrom(it => it.Organization.Org2))
                .ForMember(it => it.TaskName, config => config.MapFrom(it => it.AssetStockTaking.TaskName))
                .ForMember(it => it.TaskComment, config => config.MapFrom(it => it.AssetStockTaking.TaskComment))
                .ForMember(it => it.Progress, config => config.MapFrom(it => it.Progress()));
            //资产盘点任务和DTO之间的映射
            CreateMap<AssetStockTaking, AssetStockTakingDto>().ForMember(it => it.TimeProgress, config => config.MapFrom(it => it.TimeProgress()));
            // 员工实体和DTO之间的映射
            CreateMap<Employee, EmployeeDto>();
            //资产盘点详情和DTO之间的映射
            CreateMap<AssetStockTakingDetail, AssetStockTakingDetailDto>()
                .ForMember(it => it.AssetName, config => config.MapFrom(it => it.Asset.AssetName))
                .ForMember(it => it.AssetDescription, config => config.MapFrom(it => it.Asset.AssetDescription))
                .ForMember(it => it.AssetTagNumber, config => config.MapFrom(it => it.Asset.AssetTagNumber));
            //服务类和DTO之间的映射
            CreateMap<Maintainer, MaintainerDto>()
                .ForMember(it => it.CategoryFirstLevel,
                    config => config.MapFrom(it => it.AssetCategory.AssetFirstLevelCategory))
                .ForMember(it => it.CategorySecondLevel,
                    config => config.MapFrom(it => it.AssetCategory.AssetSecondLevelCategory))
                .ForMember(it => it.CategoryThirdLevel,
                    config => config.MapFrom(it => it.AssetCategory.AssetThirdLevelCategory));
            //权限和权限DTO
            CreateMap<Permission, PermissionDto>();
        }
    }
}