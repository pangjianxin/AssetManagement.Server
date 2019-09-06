using Boc.Assets.Domain.Commands.Validations.Assets;
using System;

namespace Boc.Assets.Domain.Commands.Assets
{
    public class CreateAssetExchangeCommand : AssetCommand
    {
        public CreateAssetExchangeCommand(
            Guid targetOrgId,
            Guid exchangeOrgId,
            Guid assetId,
            string message)
        {
            TargetOrgId = targetOrgId;
            ExchangeOrgId = exchangeOrgId;
            AssetId = assetId;
            Message = message;
        }
        public Guid ExchangeOrgId { get; set; }
        public Guid TargetOrgId { get; set; }
        public override bool IsValid()
        {
            ValidationResult = new CreateAssetExchangeCommandValidator().Validate(this);
            return ValidationResult.IsValid;
        }
    }
}