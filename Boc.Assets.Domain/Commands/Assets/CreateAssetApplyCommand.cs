using Boc.Assets.Domain.Commands.Validations.Assets;
using System;

namespace Boc.Assets.Domain.Commands.Assets
{
    public class CreateAssetApplyCommand : AssetCommand
    {
        public Guid TargetOrgId { get; set; }

        public CreateAssetApplyCommand(Guid targetOrgId, Guid assetCategoryId, string message)
        {
            AssetCategoryId = assetCategoryId;
            TargetOrgId = targetOrgId;
        }
        public override bool IsValid()
        {
            ValidationResult = new CreateAssetApplyCommandValidator().Validate(this);
            return ValidationResult.IsValid;
        }
    }
}