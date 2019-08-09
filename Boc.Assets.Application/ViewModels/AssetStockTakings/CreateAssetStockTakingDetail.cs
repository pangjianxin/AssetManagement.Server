using System;

namespace Boc.Assets.Application.ViewModels.AssetStockTakings
{
    public class CreateAssetStockTakingDetail : AssetStockTakingViewModel
    {
        public Guid AssetId { get; set; }
        public Guid AssetStockTakingOrganizationId { get; set; }
        public string ResponsibilityIdentity { get; set; }
        public string ResponsibilityName { get; set; }
        public string ResponsibilityOrg2 { get; set; }
        public string AssetStockTakingLocation { get; set; }
        public int StockTakingStatus { get; set; }
        public string Message { get; set; }
    }
}