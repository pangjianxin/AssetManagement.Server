using System;

namespace Boc.Assets.Application.ViewModels.AssetInventories
{
    public abstract class AssetInventoryViewModel : ViewModel
    {
        public Guid PublisherId { get; set; }
        public string TaskName { get; set; }
        public string TaskComment { get; set; }
        public DateTime ExpiryDateTime { get; set; }
    }
}