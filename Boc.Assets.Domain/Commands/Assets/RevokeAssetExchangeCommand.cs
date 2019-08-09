using Boc.Assets.Domain.Core.SharedKernel;
using Boc.Assets.Domain.Validations.Assets;
using System;
using System.Threading.Tasks;

namespace Boc.Assets.Domain.Commands.Assets
{
    public class RevokeAssetExchangeCommand : AssetCommand
    {
        public RevokeAssetExchangeCommand(IUser principal, Guid eventId, string message) : base(principal)
        {
            EventId = eventId;
            Message = message;
        }
        public override async Task<bool> IsValid()
        {
            ValidationResult = await new RevokeAssetExchangeCommandValidator().ValidateAsync(this);
            return ValidationResult.IsValid;
        }
    }
}