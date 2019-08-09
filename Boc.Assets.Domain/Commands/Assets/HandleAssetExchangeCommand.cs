using Boc.Assets.Domain.Core.SharedKernel;
using Boc.Assets.Domain.Validations.Assets;
using System;
using System.Threading.Tasks;

namespace Boc.Assets.Domain.Commands.Assets
{
    public class HandleAssetExchangeCommand : AssetCommand
    {

        public HandleAssetExchangeCommand(IUser principal, Guid eventId) : base(principal)
        {
            EventId = eventId;
        }
        public override async Task<bool> IsValid()
        {
            ValidationResult = await new HandleAssetExchangeCommandValidator().ValidateAsync(this);
            return ValidationResult.IsValid;
        }
    }
}