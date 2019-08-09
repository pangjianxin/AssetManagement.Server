using Boc.Assets.Domain.Commands.Organization;
using FluentValidation;

namespace Boc.Assets.Domain.Validations.Organization
{
    public class ChangeOrgPasswordCommandValidator : OrganizationCommandValidator<ChangeOrgPasswordCommand>
    {
        private void ValidateOldPassword()
        {
            RuleFor(it => it.OldPassword).NotNull().WithMessage("旧密码不能为空");
        }

        private void ValidateNewPassword()
        {
            RuleFor(it => it.NewPassword).NotNull().WithMessage("新密码不能为空");
        }

        private void ValidateConfirmPassword()
        {
            RuleFor(it => it.ConfirmPassword).Equal(it => it.NewPassword).WithMessage("确认密码和新密码不一致，请重新输入");
        }
        public ChangeOrgPasswordCommandValidator()
        {
            ValidateOrgIdentifier();
            ValidateOldPassword();
            ValidateNewPassword();
            ValidateConfirmPassword();
            ValidatePrincipal();
        }
    }
}