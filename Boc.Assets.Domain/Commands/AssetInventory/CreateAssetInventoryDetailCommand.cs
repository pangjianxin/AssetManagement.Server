using Boc.Assets.Domain.Commands.Validations.AssetInventories;
using Boc.Assets.Domain.Models.AssetInventories;
using System;

namespace Boc.Assets.Domain.Commands.AssetInventory
{
    /// <summary>
    /// 机构/网点创建一个资产盘点报告的详情
    /// </summary>
    public class CreateAssetInventoryDetailCommand : AssetInventoryCommand
    {

        public CreateAssetInventoryDetailCommand(Guid assetId,
            Guid assetInventoryRegisterId,
            string identifier,
            string name,
            string org2,
            string location,
            InventoryStatus status,
            string message)
        {
            AssetId = assetId;
            AssetInventoryRegisterId = assetInventoryRegisterId;
            ResponsibilityIdentity = identifier;
            ResponsibilityName = name;
            ResponsibilityOrg2 = org2;
            AssetInventoryLocation = location;
            InventoryStatus = status;
            Message = message;
        }
        public Guid AssetId { get; set; }
        public Guid AssetInventoryRegisterId { get; set; }
        public string ResponsibilityIdentity { get; set; }
        public string ResponsibilityName { get; set; }
        public string ResponsibilityOrg2 { get; set; }
        public string AssetInventoryLocation { get; set; }
        public InventoryStatus InventoryStatus { get; set; }
        public string Message { get; set; }
        public override bool IsValid()
        {
            ValidationResult = new CreateAssetInventoryDetailCommandValidator().Validate(this);
            return ValidationResult.IsValid;
        }
    }
}