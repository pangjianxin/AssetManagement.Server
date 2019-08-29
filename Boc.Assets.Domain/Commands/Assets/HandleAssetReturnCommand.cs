using Boc.Assets.Domain.Commands.Validations.Assets;
using Boc.Assets.Domain.Core.SharedKernel;
using System;

namespace Boc.Assets.Domain.Commands.Assets
{
    public class HandleAssetReturnCommand : AssetCommand
    {

        public HandleAssetReturnCommand(IUser principal, Guid eventId) : base(principal)
        {
            EventId = eventId;
        }

        public override bool IsValid()
        {
            ValidationResult = new HandleAssetReturnCommandValidator().Validate(this);
            return ValidationResult.IsValid;
        }
    }
}