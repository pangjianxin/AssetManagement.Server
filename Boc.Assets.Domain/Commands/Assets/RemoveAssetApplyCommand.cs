using Boc.Assets.Domain.Core.SharedKernel;
using Boc.Assets.Domain.Validations.Assets;
using System;
using System.Threading.Tasks;

namespace Boc.Assets.Domain.Commands.Assets
{
    public class RemoveAssetApplyCommand : AssetCommand
    {
        public RemoveAssetApplyCommand(IUser principal, Guid eventId) : base(principal)
        {
            EventId = eventId;
        }

        public override async Task<bool> IsValid()
        {
            ValidationResult = await new RemoveAssetApplyCommandValidator().ValidateAsync(this);
            return ValidationResult.IsValid;
        }
    }
}