using Boc.Assets.Domain.Commands.Organization;
using FluentValidation;

namespace Boc.Assets.Domain.Commands.Validations.Organization
{
    public class ChangeOrgShortNameCommandValidator : AbstractValidator<ChangeOrgShortNameCommand>
    {
        private void ValidateOrgIdentifier()
        {
            RuleFor(it => it.OrgIdentifier).Must(it => !string.IsNullOrEmpty(it))
                .WithMessage("机构号不能为空");
        }
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