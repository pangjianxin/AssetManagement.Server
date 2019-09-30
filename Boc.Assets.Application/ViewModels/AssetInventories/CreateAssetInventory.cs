using System;
using System.Collections.Generic;

namespace Boc.Assets.Application.ViewModels.AssetInventories
{
    public class CreateAssetInventory : AssetInventoryViewModel
    {
        public IEnumerable<Guid> ExcludedOrganizations { get; set; }
    }
}