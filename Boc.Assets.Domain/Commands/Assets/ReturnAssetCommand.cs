using Boc.Assets.Domain.Commands.Validations.Assets;
using System;

namespace Boc.Assets.Domain.Commands.Assets
{
    public class ReturnAssetCommand : AssetCommand
    {
        public Guid TargetOrgId { get; set; }

        public ReturnAssetCommand(Guid targetOrgId, Guid assetId, string message)
        {
            TargetOrgId = targetOrgId;
            AssetId = assetId;
            Message = message;
        }

        public override bool IsValid()
        {
            ValidationResult = new ReturnAssetCommandValidator().Validate(this);
            return ValidationResult.IsValid;
        }
    }
}