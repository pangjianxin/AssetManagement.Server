using Boc.Assets.Domain.Core.Commands;
using Boc.Assets.Domain.Core.SharedKernel;
using System;

namespace Boc.Assets.Domain.Commands.AssetStockTaking
{
    public abstract class AssetStockTakingCommand : Command
    {
        public AssetStockTakingCommand(IUser principal) : base(principal)
        {

        }
        public string TaskName { get; set; }
        public string TaskComment { get; set; }
        public DateTime ExpiryDateTime { get; set; }
    }
}
