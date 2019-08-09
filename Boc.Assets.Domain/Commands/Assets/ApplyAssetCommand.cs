using Boc.Assets.Domain.Core.SharedKernel;
using Boc.Assets.Domain.Validations.Assets;
using System;
using System.Threading.Tasks;

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
        public override async Task<bool> IsValid()
        {
            ValidationResult = await new ApplyAssetCommandValidator().ValidateAsync(this);
            return ValidationResult.IsValid;
        }
    }
}