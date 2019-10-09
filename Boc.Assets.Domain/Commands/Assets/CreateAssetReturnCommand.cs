using Boc.Assets.Domain.Commands.Validations.Assets;
using System;

namespace Boc.Assets.Domain.Commands.Assets
{
    public class CreateAssetReturnCommand : ApplyCommand
    {

        public CreateAssetReturnCommand(Guid targetOrgId, Guid assetId, string message)
        {
            TargetOrgId = targetOrgId;
            AssetId = assetId;
            Message = message;
        }

        public override bool IsValid()
        {
            ValidationResult = new CreateAssetReturnCommandValidator().Validate(this);
            return ValidationResult.IsValid;
        }
    }
}