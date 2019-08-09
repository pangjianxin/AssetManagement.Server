using System;
using System.Collections.Generic;

namespace Boc.Assets.Application.ViewModels.AssetStockTakings
{
    public class CreateAssetStockTaking : AssetStockTakingViewModel
    {
        public IEnumerable<Guid> ExcludedOrganizations { get; set; }
    }
}