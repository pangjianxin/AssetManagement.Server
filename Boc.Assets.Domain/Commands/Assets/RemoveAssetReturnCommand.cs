using Boc.Assets.Domain.Core.SharedKernel;

namespace Boc.Assets.Domain.Commands.Assets
{
    public class RemoveAssetReturnCommand : AssetCommand
    {
        public RemoveAssetReturnCommand(IUser principal) : base(principal)
        {
        }

        public override bool IsValid()
        {
            throw new System.NotImplementedException();
        }
    }
}