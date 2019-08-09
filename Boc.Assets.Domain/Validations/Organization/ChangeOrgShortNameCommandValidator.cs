using Boc.Assets.Domain.Commands.Organization;
using FluentValidation;

namespace Boc.Assets.Domain.Validations.Organization
{
    public class ChangeOrgShortNameCommandValidator : OrganizationCommandValidator<ChangeOrgShortNameCommand>
    {
        private void ValidateShortName()
        {
            RuleFor(it => it.OrgShortNam).NotNull().WithMessage("机构简称不能为空");
        }
        public ChangeOrgShortNameCommandValidator()
        {
            ValidateOrgIdentifier();
            ValidateShortName();
        }
    }
}