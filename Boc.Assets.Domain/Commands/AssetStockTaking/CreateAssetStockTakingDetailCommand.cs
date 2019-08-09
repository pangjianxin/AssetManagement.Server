using Boc.Assets.Domain.Core.SharedKernel;
using Boc.Assets.Domain.Models.AssetStockTakings;
using Boc.Assets.Domain.Validations.AssetStockTakings;
using System;
using System.Threading.Tasks;

namespace Boc.Assets.Domain.Commands.AssetStockTaking
{
    /// <summary>
    /// 机构/网点创建一个资产盘点报告的详情
    /// </summary>
    public class CreateAssetStockTakingDetailCommand : AssetStockTakingCommand
    {

        public CreateAssetStockTakingDetailCommand(IUser principal, Guid assetId,
            Guid stockTakingOrgId,
            string identifier,
            string name,
            string org2,
            string location,
            StockTakingStatus status,
            string message) : base(principal)
        {
            AssetId = assetId;
            AssetStockTakingOrganizationId = stockTakingOrgId;
            ResponsibilityIdentity = identifier;
            ResponsibilityName = name;
            ResponsibilityOrg2 = org2;
            AssetStockTakingLocation = location;
            StockTakingStatus = status;
            Message = message;
        }
        public Guid AssetId { get; set; }
        public Guid AssetStockTakingOrganizationId { get; set; }
        public string ResponsibilityIdentity { get; set; }
        public string ResponsibilityName { get; set; }
        public string ResponsibilityOrg2 { get; set; }
        public string AssetStockTakingLocation { get; set; }
        public StockTakingStatus StockTakingStatus { get; set; }
        public string Message { get; set; }
        public override async Task<bool> IsValid()
        {
            ValidationResult = await new CreateAssetStockTakingDetailCommandValidators().ValidateAsync(this);
            return ValidationResult.IsValid;
        }
    }
}