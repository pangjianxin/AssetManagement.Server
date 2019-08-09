using Boc.Assets.Domain.Core.SharedKernel;
using Boc.Assets.Domain.Validations.Assets;
using System;
using System.Threading.Tasks;

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
        public override async Task<bool> IsValid()
        {
            ValidationResult = await new ExchangeAssetCommandValidator().ValidateAsync(this);
            return ValidationResult.IsValid;
        }
    }
}