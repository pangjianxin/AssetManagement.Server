using Boc.Assets.Domain.Commands.Validations.Assets;
using Boc.Assets.Domain.Core.SharedKernel;
using System;

namespace Boc.Assets.Domain.Commands.Assets
{
    public class RevokeAssetReturnCommand : AssetCommand
    {
        public RevokeAssetReturnCommand(IUser principal, Guid eventId, string message) : base(principal)
        {
            EventId = eventId;
            Message = message;
        }
        public override bool IsValid()
        {
            ValidationResult = new RevokeAssetReturnCommandValidator().Validate(this);
            return ValidationResult.IsValid;
        }
    }
}