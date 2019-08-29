using Boc.Assets.Domain.Commands.Validations.Assets;
using Boc.Assets.Domain.Core.SharedKernel;
using System;

namespace Boc.Assets.Domain.Commands.Assets
{
    public class ExchangeAssetCommand : AssetCommand
    {
        public ExchangeAssetCommand(IUser principal,
            Guid targetOrgId,
            Guid exchangeOrgId,
            Guid assetId,
            string message) : base(principal)
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
            ValidationResult = new ExchangeAssetCommandValidator().Validate(this);
            return ValidationResult.IsValid;
        }
    }
}