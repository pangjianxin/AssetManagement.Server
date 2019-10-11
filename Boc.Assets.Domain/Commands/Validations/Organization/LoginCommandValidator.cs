using Boc.Assets.Domain.Commands.Organization;
using FluentValidation;

namespace Boc.Assets.Domain.Commands.Validations.Organization
{
    public class LoginCommandValidator : AbstractValidator<LoginCommand>
    {
        private void ValidatePassword()
        {
            RuleFor(it => it.Password).Must(it => !string.IsNullOrEmpty(it) && !string.IsNullOrWhiteSpace(it))
                .WithMessage("密码不能为空");
        }

        private void ValidateOrgIdentifier()
        {
            RuleFor(it => it.OrgIdentifier).Must(it => !string.IsNullOrEmpty(it) && !string.IsNullOrWhiteSpace(it))
                .WithMessage("机构号不能为空");
        }
        public LoginCommandValidator()
        {
            ValidateOrgIdentifier();
            ValidatePassword();
        }
    }
}