using Boc.Assets.Domain.Core.SharedKernel;
using Boc.Assets.Domain.Validations.Assets;
using System;
using System.Threading.Tasks;

namespace Boc.Assets.Domain.Commands.Assets
{
    public class HandleAssetReturnCommand : AssetCommand
    {

        public HandleAssetReturnCommand(IUser principal, Guid eventId) : base(principal)
        {
            EventId = eventId;
        }

        public override async Task<bool> IsValid()
        {
            ValidationResult = await new HandleAssetReturnCommandValidator().ValidateAsync(this);
            return ValidationResult.IsValid;
        }
    }
}