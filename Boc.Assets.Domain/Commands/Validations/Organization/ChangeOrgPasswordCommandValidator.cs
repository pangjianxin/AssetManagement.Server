using Boc.Assets.Domain.Commands.Organization;
using FluentValidation;

namespace Boc.Assets.Domain.Commands.Validations.Organization
{
    public class ChangeOrgPasswordCommandValidator : AbstractValidator<ChangeOrgPasswordCommand>
    {
        private void ValidateOldPassword()
        {
            RuleFor(it => it.OldPassword).NotNull().WithMessage("旧密码不能为空");
        }

        private void ValidateNewPassword()
        {
            RuleFor(it => it.NewPassword).NotNull()
                .WithMessage("新密码不能为空");
        }

        private void ValidateConfirmPassword()
        {
            RuleFor(it => it.ConfirmPassword).Equal(it => it.NewPassword)
                .WithMessage("确认密码和新密码不一致，请重新输入");
        }

        private void ValidateOrgIdentifier()
        {
            RuleFor(it => it.OrgIdentifier).Must(it => !string.IsNullOrEmpty(it))
                .WithMessage("机构号不能为空");
        }
        public ChangeOrgPasswordCommandValidator()
        {
            ValidateOrgIdentifier();
            ValidateOldPassword();
            ValidateNewPassword();
            ValidateConfirmPassword();

        }
    }
}