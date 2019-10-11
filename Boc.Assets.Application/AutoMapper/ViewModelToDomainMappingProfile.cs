using AutoMapper;
using Boc.Assets.Application.ViewModels.AssetCategory;
using Boc.Assets.Application.ViewModels.AssetInventories;
using Boc.Assets.Application.ViewModels.Assets;
using Boc.Assets.Application.ViewModels.Employee;
using Boc.Assets.Application.ViewModels.Login;
using Boc.Assets.Application.ViewModels.Maintainers;
using Boc.Assets.Application.ViewModels.Organization;
using Boc.Assets.Application.ViewModels.OrganizationSpace;
using Boc.Assets.Application.ViewModels.Permission;
using Boc.Assets.Domain.Commands.AssetCategory;
using Boc.Assets.Domain.Commands.AssetInventory;
using Boc.Assets.Domain.Commands.Assets;
using Boc.Assets.Domain.Commands.Employee;
using Boc.Assets.Domain.Commands.Maintainers;
using Boc.Assets.Domain.Commands.Organization;
using Boc.Assets.Domain.Commands.OrganizationSpace;
using Boc.Assets.Domain.Commands.Permissions;
using Boc.Assets.Domain.Models.AssetInventories;

namespace Boc.Assets.Application.AutoMapper
{
    public class ViewModelToDomainMappingProfile : Profile
    {
        public ViewModelToDomainMappingProfile()
        {
            //登录
            CreateMap<Login, LoginCommand>()
                .ConstructUsing(c => new LoginCommand(c.OrgIdentifier, c.Password));
            //修改机构简称
            CreateMap<ChangeOrgShortName, ChangeOrgShortNameCommand>()
                .ConstructUsing(c => new ChangeOrgShortNameCommand(c.OrgIdentifier, c.OrgShortNam));
            //重置机构密码
            CreateMap<ResetOrgPassword, ResetOrgPasswordCommand>()
                .ConstructUsing(c => new ResetOrgPasswordCommand(c.OrgIdentifier));
            //修改机构密码
            CreateMap<ChangeOrgPassword, ChangeOrgPasswordCommand>()
                .ConstructUsing(c => new ChangeOrgPasswordCommand(c.OrgIdentifier,
                    c.OldPassword, c.NewPassword, c.ConfirmPassword));
            //修改分类的单位
            CreateMap<ChangeMeteringUnit, ChangeMeteringUnitCommand>()
                .ConstructUsing(c => new ChangeMeteringUnitCommand(c.AssetCategoryId, c.AssetMeteringUnit));
            //创建机构空间
            CreateMap<CreateSpace, CreateSpaceCommand>()
                .ConstructUsing(c => new CreateSpaceCommand(c.SpaceName, c.SpaceDescription));
            //修改空间信息
            CreateMap<ModifySpaceInfo, ModifySpaceInfoCommand>()
                .ConstructUsing(c => new ModifySpaceInfoCommand(c.SpaceId, c.SpaceName, c.SpaceDescription));
            //修改资产位置
            CreateMap<ModifyAssetLocation, ModifyAssetLocationCommand>()
                .ConstructUsing(c => new ModifyAssetLocationCommand(c.AssetId, c.AssetLocation));
            //创建资产申请
            CreateMap<ApplyAsset, CreateAssetApplyCommand>()
                .ConstructUsing(c => new CreateAssetApplyCommand(c.TargetOrgId, c.AssetCategoryId, c.Message));
            //处理资产申请
            CreateMap<HandleAssetApply, HandleAssetApplyCommand>()
                .ConstructUsing(c => new HandleAssetApplyCommand(c.EventId, c.AssetId));
            //创建资产交回申请
            CreateMap<ReturnAsset, CreateAssetReturnCommand>()
                .ConstructUsing(c => new CreateAssetReturnCommand(c.TargetOrgId, c.AssetId, c.Message));
            //处理资产交回申请
            CreateMap<HandleAssetReturn, HandleAssetReturnCommand>()
                .ConstructUsing(c => new HandleAssetReturnCommand(c.EventId));
            //创建资产机构间调配申请
            CreateMap<ExchangeAsset, CreateAssetExchangeCommand>()
                .ConstructUsing(c => new CreateAssetExchangeCommand(c.TargetOrgId, c.ExchangeOrgId, c.AssetId, c.Message));
            //处理资产机构间调配申请
            CreateMap<HandleAssetExchange, HandleAssetExchangeCommand>()
                .ConstructUsing(c => new HandleAssetExchangeCommand(c.EventId));
            //撤销资产申请
            CreateMap<RevokeAssetApply, RevokeAssetApplyCommand>()
                .ConstructUsing(c => new RevokeAssetApplyCommand(c.EventId, c.Message));
            //撤销资产交回申请
            CreateMap<RevokeAssetReturn, RevokeAssetReturnCommand>()
                .ConstructUsing(c => new RevokeAssetReturnCommand(c.EventId, c.Message));
            //撤销资产调配申请
            CreateMap<RevokeAssetExchange, RevokeAssetExchangeCommand>()
                .ConstructUsing(c => new RevokeAssetExchangeCommand(c.EventId, c.Message));
            //资产入库
            CreateMap<StoreAsset, StoreAssetWithOutFileCommand>()
                .ConstructUsing(c => new StoreAssetWithOutFileCommand(c.AssetName, c.Brand, c.AssetDescription, c.AssetType, c.AssetLocation, c.AssetCategoryId, c.CreateDateTime, c.StartTagNumber, c.EndTagNumber));
            //创建资产盘点
            CreateMap<CreateAssetInventory, CreateAssetInventoryCommand>()
                .ConstructUsing(c =>
                    new CreateAssetInventoryCommand(c.TaskName, c.TaskComment, c.ExpiryDateTime, c.ExcludedOrganizations));
            //添加员工信息
            CreateMap<AddEmployee, AddEmployeeCommand>()
                .ConstructUsing(c => new AddEmployeeCommand(c.Name, c.Identifier, c.Org2, c.Telephone, c.OfficePhone));
            //创建资产盘点明细
            CreateMap<CreateAssetInventoryDetail, CreateAssetInventoryDetailCommand>()
                .ConstructUsing(c => new CreateAssetInventoryDetailCommand(c.AssetId,
                    c.AssetInventoryRegisterId,
                    c.ResponsibilityIdentity,
                    c.ResponsibilityName,
                    c.ResponsibilityOrg2,
                    c.AssetInventoryLocation,
                    (InventoryStatus)c.InventoryStatus,
                    c.Message));
            //添加资产维修商信息
            CreateMap<AddMaintainer, AddMaintainerCommand>()
                .ConstructUsing(c => new AddMaintainerCommand(c.OrganizationId, c.Org2, c.AssetCategoryId, c.CompanyName,
                    c.MaintainerName, c.Telephone, c.OfficePhone));
            //删除资产维修商信息
            CreateMap<DeleteMaintainer, DeleteMaintainerCommand>()
                .ConstructUsing(c => new DeleteMaintainerCommand(c.MaintainerId));
            //删除资产申请
            CreateMap<RemoveAssetApply, RemoveAssetApplyCommand>()
                .ConstructUsing(c => new RemoveAssetApplyCommand(c.EventId));
            //删除资产交回申请
            CreateMap<RemoveAssetReturn, RemoveAssetReturnCommand>()
                .ConstructUsing(c => new RemoveAssetReturnCommand(c.EventId));
            //删除资产调配申请
            CreateMap<RemoveAssetExchange, RemoveAssetExchangeCommand>()
                .ConstructUsing(c => new RemoveAssetExchangeCommand(c.EventId));
            //修改权限
            CreateMap<ModifyPermission, ModifyPermissionCommand>();


        }
    }
}