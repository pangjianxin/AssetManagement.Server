using System;

namespace Boc.Assets.Application.ViewModels.Assets
{
    public class ExchangeAsset : AssetViewModel
    {
        public Guid TargetOrgId { get; set; }
        public Guid ExchangeOrgId { get; set; }
    }
}