using Boc.Assets.Domain.Commands.Validations.Maintainers;
using Boc.Assets.Domain.Core.SharedKernel;
using System;

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
        public override bool IsValid()
        {
            ValidationResult = new AddMaintainerCommandValidator().Validate(this);
            return ValidationResult.IsValid;
        }
    }
}