using Boc.Assets.Domain.Commands.Validations.Assets;
using Boc.Assets.Domain.Core.SharedKernel;
using System;

namespace Boc.Assets.Domain.Commands.Assets
{
    public class ApplyAssetCommand : AssetCommand
    {
        public Guid TargetOrgId { get; set; }

        public ApplyAssetCommand(IUser principal, Guid targetOrgId, Guid assetCategoryId, string message)
            : base(principal)
        {
            AssetCategoryId = assetCategoryId;
            TargetOrgId = targetOrgId;
        }
        public override bool IsValid()
        {
            ValidationResult = new ApplyAssetCommandValidator().Validate(this);
            return ValidationResult.IsValid;
        }
    }
}