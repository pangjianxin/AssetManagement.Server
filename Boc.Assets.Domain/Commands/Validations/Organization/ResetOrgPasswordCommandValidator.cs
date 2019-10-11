using Boc.Assets.Domain.Commands.Organization;
using FluentValidation;

namespace Boc.Assets.Domain.Commands.Validations.Organization
{
    public class ResetOrgPasswordCommandValidator : AbstractValidator<ResetOrgPasswordCommand>
    {
        private void ValidateOrgIdentifier()
        {
            RuleFor(it => it.OrgIdentifier).Must(it => !string.IsNullOrEmpty(it))
                .WithMessage("机构号不能为空");
        }
        public ResetOrgPasswordCommandValidator()
        {
            ValidateOrgIdentifier();
        }
    }
}