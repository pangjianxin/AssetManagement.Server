using Boc.Assets.Domain.Core.Commands;
using System;

namespace Boc.Assets.Domain.Commands.AssetStockTaking
{
    public abstract class AssetStockTakingCommand : Command
    {
        public string TaskName { get; set; }
        public string TaskComment { get; set; }
        public DateTime ExpiryDateTime { get; set; }
    }
}
