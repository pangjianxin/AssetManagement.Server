using Boc.Assets.Domain.Core.Commands;
using Boc.Assets.Domain.Core.SharedKernel;
using Boc.Assets.Domain.Models.Assets;
using System;

namespace Boc.Assets.Domain.Commands.AssetCategory
{
    public abstract class AssetCategoryCommand : Command
    {
        protected AssetCategoryCommand(IUser principal) : base(principal)
        {

        }
        public Guid AssetCategoryId { get; set; }
        public AssetMeteringUnit AssetMeteringUnit { get; set; }
        public string AssetFirstLevelCategory { get; set; }
        public string AssetSecondLevelCategory { get; set; }
        public string AssetThirdLevelCategory { get; set; }
    }
}