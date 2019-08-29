using Boc.Assets.Domain.Commands.Validations.Assets;
using Boc.Assets.Domain.Core.SharedKernel;
using System;

namespace Boc.Assets.Domain.Commands.Assets
{
    public class ReturnAssetCommand : AssetCommand
    {
        public Guid TargetOrgId { get; set; }

        public ReturnAssetCommand(IUser principal, Guid targetOrgId, Guid assetId, string message) : base(principal)
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