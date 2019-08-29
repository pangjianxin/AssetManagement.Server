using AutoMapper;
using Boc.Assets.Application.ViewModels.AssetCategory;
using Boc.Assets.Application.ViewModels.Assets;
using Boc.Assets.Application.ViewModels.AssetStockTakings;
using Boc.Assets.Application.ViewModels.Employee;
using Boc.Assets.Application.ViewModels.Maintainers;
using Boc.Assets.Application.ViewModels.Organization;
using Boc.Assets.Application.ViewModels.OrganizationSpace;
using Boc.Assets.Application.ViewModels.Permission;
using Boc.Assets.Domain.Commands.AssetCategory;
using Boc.Assets.Domain.Commands.Assets;
using Boc.Assets.Domain.Commands.AssetStockTaking;
using Boc.Assets.Domain.Commands.Employee;
using Boc.Assets.Domain.Commands.Maintainers;
using Boc.Assets.Domain.Commands.Organization;
using Boc.Assets.Domain.Commands.OrganizationSpace;
using Boc.Assets.Domain.Commands.Permissions;
using Boc.Assets.Domain.Models.AssetStockTakings;

namespace Boc.Assets.Application.AutoMapper
{
    public class ViewModelToDomainMappingProfile : Profile
    {
        public ViewModelToDomainMappingProfile()
        {
            CreateMap<ChangeOrgShortName, ChangeOrgShortNameCommand>()
                .ConstructUsing(c => new ChangeOrgShortNameCommand(c.OrgIdentifier, c.OrgShortNam));
            CreateMap<ResetOrgPassword, ResetOrgPasswordCommand>()
                .ConstructUsing(c => new ResetOrgPasswordCommand(c.OrgIdentifier));
            CreateMap<ChangeOrgPassword, ChangeOrgPasswordCommand>()
                .ConstructUsing(c => new ChangeOrgPasswordCommand(c.OrgIdentifier,
                    c.OldPassword, c.NewPassword, c.ConfirmPassword));
            CreateMap<ChangeMeteringUnit, ChangeMeteringUnitCommand>()
                .ConstructUsing(c => new ChangeMeteringUnitCommand(c.AssetCategoryId, c.AssetMeteringUnit));
            CreateMap<CreateSpace, CreateSpaceCommand>()
                .ConstructUsing(c => new CreateSpaceCommand(c.SpaceName, c.SpaceDescription));
            CreateMap<ModifySpaceInfo, ModifySpaceInfoCommand>()
                .ConstructUsing(c => new ModifySpaceInfoCommand(c.SpaceId, c.SpaceName, c.SpaceDescription));
            CreateMap<ModifyAssetLocation, ModifyAssetLocationCommand>()
                .ConstructUsing(c => new ModifyAssetLocationCommand(c.AssetId, c.AssetLocation));
            CreateMap<ApplyAsset, ApplyAssetCommand>()
                .ConstructUsing(c => new ApplyAssetCommand(c.TargetOrgId, c.AssetCategoryId, c.Message));
            CreateMap<HandleAssetApply, HandleAssetApplyCommand>()
                .ConstructUsing(c => new HandleAssetApplyCommand(c.EventId, c.AssetId));
            CreateMap<ReturnAsset, ReturnAssetCommand>()
                .ConstructUsing(c => new ReturnAssetCommand(c.TargetOrgId, c.AssetId, c.Message));
            CreateMap<HandleAssetReturn, HandleAssetReturnCommand>()
                .ConstructUsing(c => new HandleAssetReturnCommand(c.EventId));
            CreateMap<ExchangeAsset, ExchangeAssetCommand>()
                .ConstructUsing(c => new ExchangeAssetCommand(c.TargetOrgId, c.ExchangeOrgId, c.AssetId, c.Message));
            CreateMap<HandleAssetExchange, HandleAssetExchangeCommand>()
                .ConstructUsing(c => new HandleAssetExchangeCommand(c.EventId));
            CreateMap<Revoke, RevokeAssetApplyCommand>()
                .ConstructUsing(c => new RevokeAssetApplyCommand(c.EventId, c.Message));
            CreateMap<Revoke, RevokeAssetReturnCommand>()
                .ConstructUsing(c => new RevokeAssetReturnCommand(c.EventId, c.Message));
            CreateMap<Revoke, RevokeAssetExchangeCommand>()
                .ConstructUsing(c => new RevokeAssetExchangeCommand(c.EventId, c.Message));
            CreateMap<StoreAsset, StoreAssetWithOutFileCommand>()
                .ConstructUsing(c => new StoreAssetWithOutFileCommand(c.AssetName, c.Brand, c.AssetDescription, c.AssetType, c.AssetLocation, c.AssetCategoryId, c.CreateDateTime, c.StartTagNumber, c.EndTagNumber));
            CreateMap<CreateAssetStockTaking, CreateAssetStockTakingCommand>()
                .ConstructUsing(c =>
                    new CreateAssetStockTakingCommand(c.TaskName, c.TaskComment, c.ExpiryDateTime, c.ExcludedOrganizations));
            CreateMap<AddEmployee, AddEmployeeCommand>()
                .ConstructUsing(c => new AddEmployeeCommand(c.Name, c.Identifier, c.Org2, c.Telephone, c.OfficePhone));
            CreateMap<CreateAssetStockTakingDetail, CreateAssetStockTakingDetailCommand>()
                .ConstructUsing(c => new CreateAssetStockTakingDetailCommand(c.AssetId,
                    c.AssetStockTakingOrganizationId,
                    c.ResponsibilityIdentity,
                    c.ResponsibilityName,
                    c.ResponsibilityOrg2,
                    c.AssetStockTakingLocation,
                    (StockTakingStatus)c.StockTakingStatus,
                    c.Message));
            CreateMap<AddMaintainer, AddMaintainerCommand>()
                .ConstructUsing(c => new AddMaintainerCommand(c.OrganizationId, c.Org2, c.AssetCategoryId, c.CompanyName,
                    c.MaintainerName, c.Telephone, c.OfficePhone));
            CreateMap<DeleteMaintainer, DeleteMaintainerCommand>()
                .ConstructUsing(c => new DeleteMaintainerCommand(c.MaintainerId));
            CreateMap<Remove, RemoveAssetApplyCommand>()
                .ConstructUsing(c => new RemoveAssetApplyCommand(c.EventId));
            CreateMap<Remove, RemoveAssetReturnCommand>()
                .ConstructUsing(c => new RemoveAssetReturnCommand(c.EventId));

            CreateMap<ModifyPermission, ModifyPermissionCommand>();


        }
    }
}