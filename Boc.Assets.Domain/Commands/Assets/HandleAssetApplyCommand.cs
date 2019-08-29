using Boc.Assets.Domain.Commands.Validations.Assets;
using Boc.Assets.Domain.Core.SharedKernel;
using System;

namespace Boc.Assets.Domain.Commands.Assets
{
    public class HandleAssetApplyCommand : AssetCommand
    {

        public HandleAssetApplyCommand(IUser principal, Guid eventId, Guid assetId) : base(principal)
        {
            EventId = eventId;
            AssetId = assetId;
        }
        public override bool IsValid()
        {
            ValidationResult = new HandleAssetApplyCommandValidator().Validate(this);
            return ValidationResult.IsValid;
        }
    }
}