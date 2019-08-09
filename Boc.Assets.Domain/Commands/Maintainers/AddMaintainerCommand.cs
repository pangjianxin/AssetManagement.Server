using Boc.Assets.Domain.Core.SharedKernel;
using Boc.Assets.Domain.Validations.Maintainers;
using System;
using System.Threading.Tasks;

namespace Boc.Assets.Domain.Commands.Maintainers
{
    public class AddMaintainerCommand : MaintainerCommand
    {
        public AddMaintainerCommand(
            IUser principal,
            Guid assetCategoryId,
            string companyName,
            string maintainerName,
            string telephone,
            string officePhone = null) : base(principal)
        {
            AssetCategoryId = assetCategoryId;
            CompanyName = companyName;
            MaintainerName = maintainerName;
            Telephone = telephone;
            OfficePhone = officePhone;
            OrganizationId = principal.OrgId;
            Org2 = principal.Org2;
        }
        public override async Task<bool> IsValid()
        {
            ValidationResult = await new AddMaintainerCommandValidator().ValidateAsync(this);
            return ValidationResult.IsValid;
        }
    }
}