using Boc.Assets.Domain.Commands.Validations.Maintainers;
using System;

namespace Boc.Assets.Domain.Commands.Maintainers
{
    public class AddMaintainerCommand : MaintainerCommand
    {
        public AddMaintainerCommand(
            Guid organizationId,
            string org2,
            Guid assetCategoryId,
            string companyName,
            string maintainerName,
            string telephone,
            string officePhone = null)
        {
            OrganizationId = organizationId;
            Org2 = org2;
            AssetCategoryId = assetCategoryId;
            CompanyName = companyName;
            MaintainerName = maintainerName;
            Telephone = telephone;
            OfficePhone = officePhone;
        }
        public override bool IsValid()
        {
            ValidationResult = new AddMaintainerCommandValidator().Validate(this);
            return ValidationResult.IsValid;
        }
    }
}