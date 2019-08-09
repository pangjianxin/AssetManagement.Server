using Boc.Assets.Domain.Core.SharedKernel;
using Boc.Assets.Domain.Validations.Assets;
using System;
using System.Threading.Tasks;

namespace Boc.Assets.Domain.Commands.Assets
{
    public class HandleAssetApplyCommand : AssetCommand
    {

        public HandleAssetApplyCommand(IUser principal, Guid eventId, Guid assetId) : base(principal)
        {
            EventId = eventId;
            AssetId = assetId;
        }
        public override async Task<bool> IsValid()
        {
            ValidationResult = await new HandleAssetApplyCommandValidator().ValidateAsync(this);
            return ValidationResult.IsValid;
        }
    }
}