using System;
using Boc.Assets.Domain.Core.Commands;

namespace Boc.Assets.Domain.Commands.AssetInventory
{
    public abstract class AssetInventoryCommand : Command
    {
        public string TaskName { get; set; }
        public string TaskComment { get; set; }
        public DateTime ExpiryDateTime { get; set; }
    }
}
