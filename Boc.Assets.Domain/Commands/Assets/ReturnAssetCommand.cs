using Boc.Assets.Domain.Core.SharedKernel;
using Boc.Assets.Domain.Validations.Assets;
using System;
using System.Threading.Tasks;

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

        public override async Task<bool> IsValid()
        {
            ValidationResult = await new ReturnAssetCommandValidator().ValidateAsync(this);
            return ValidationResult.IsValid;
        }
    }
}