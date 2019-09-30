using System;

namespace Boc.Assets.Application.ViewModels.AssetInventories
{
    public class CreateAssetInventoryDetail : AssetInventoryViewModel
    {
        public Guid AssetId { get; set; }
        public Guid AssetInventoryRegisterId { get; set; }
        public string ResponsibilityIdentity { get; set; }
        public string ResponsibilityName { get; set; }
        public string ResponsibilityOrg2 { get; set; }
        public string AssetInventoryLocation { get; set; }
        public int InventoryStatus { get; set; }
        public string Message { get; set; }
    }
}