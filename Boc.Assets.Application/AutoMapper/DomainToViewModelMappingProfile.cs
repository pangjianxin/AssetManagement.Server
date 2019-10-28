using AutoMapper;
using Boc.Assets.Application.Dto;
using Boc.Assets.Domain.Models.Applies;
using Boc.Assets.Domain.Models.AssetInventories;
using Boc.Assets.Domain.Models.Assets;
using Boc.Assets.Domain.Models.Assets.TableViews;
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
                .ForMember(it => it.RoleDescription, config => config.MapFrom(it => it.Role.Description));

            //资产分类和DTO之间的映射
            CreateMap<AssetCategory, AssetCategoryDto>()
                .ForMember(it => it.AssetMeteringUnit, config => config.MapFrom(it => it.AssetMeteringUnit.ToString()));
            //资产和DTO之间的映射
            CreateMap<Asset, AssetDto>()
                .ForMember(it => it.AssetCategoryDto, config => config.MapFrom(it => it.AssetCategory));
            //机构空间和DTO之间的映射
            CreateMap<OrganizationSpace, OrgSpaceDto>();
            //资产申请事件和DTO之间的映射
            CreateMap<AssetApply, AssetApplyDto>()
                .ForMember(it => it.Status, config => config.MapFrom(it => it.Status.ToString()))
                .ForMember(it => it.DateTimeFromNow, config => config.MapFrom(it => it.DateTimeFromNow()));
            //资产交回事件和DTO之间的映射
            CreateMap<AssetReturn, AssetReturnDto>()
                .ForMember(it => it.Status, config => config.MapFrom(it => it.Status.ToString()))
                .ForMember(it => it.DateTimeFromNow, config => config.MapFrom(it => it.DateTimeFromNow()));
            //资产机构间调换和DTO之间的映射
            CreateMap<AssetExchange, AssetExchangeDto>()
                .ForMember(it => it.Status, config => config.MapFrom(it => it.Status.ToString()))
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
            CreateMap<AssetInventoryRegister, AssetInventoryRegisterDto>()
                .ForMember(it => it.Participation, config => config.MapFrom(it => it.Participation))
                .ForMember(it => it.AssetInventory, config => config.MapFrom(it => it.AssetInventory))
                .ForMember(it => it.Progress, config => config.MapFrom(it => it.Progress()));
            //资产盘点任务和DTO之间的映射
            CreateMap<AssetInventory, AssetInventoryDto>().ForMember(it => it.TimeProgress, config => config.MapFrom(it => it.TimeProgress()));
            // 员工实体和DTO之间的映射
            CreateMap<Employee, EmployeeDto>();
            //资产盘点详情和DTO之间的映射
            CreateMap<AssetInventoryDetail, AssetInventoryDetailDto>()
                .ForMember(it => it.AssetName, config => config.MapFrom(it => it.Asset.AssetName))
                .ForMember(it => it.AssetDescription, config => config.MapFrom(it => it.Asset.AssetDescription))
                .ForMember(it => it.AssetTagNumber, config => config.MapFrom(it => it.Asset.AssetTagNumber))
                .ForMember(it => it.InventoryStatus, config => config.MapFrom(it => it.InventoryStatus.ToString()));
            //服务类和DTO之间的映射
            CreateMap<Maintainer, MaintainerDto>()
                .ForMember(it => it.CategoryFirstLevel,
                    config => config.MapFrom(it => it.AssetCategory.AssetFirstLevelCategory))
                .ForMember(it => it.CategorySecondLevel,
                    config => config.MapFrom(it => it.AssetCategory.AssetSecondLevelCategory))
                .ForMember(it => it.CategoryThirdLevel,
                    config => config.MapFrom(it => it.AssetCategory.AssetThirdLevelCategory));
            //资产信息汇总信息映射到图表数据
            CreateMap<AssetSumarryByCategory, ChartData>().ConstructUsing(c =>
                new ChartData(c.AssetThirdLevelCategory, c.AssetCount.ToString(), c.AssetCategoryId.ToString()));
            CreateMap<AssetSumarryByCount, ChartData>().ConstructUsing(c =>
                new ChartData(c.OrgInUseIdentifier, c.AssetCount.ToString(), c.OrganizationInUseId.ToString()));
        }
    }
}