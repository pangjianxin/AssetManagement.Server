using Boc.Assets.Application.ViewModels.Organization;
using FluentValidation;

namespace Boc.Assets.Application.FluentValidations.Organization
{
    public class ChangeOrgPasswordValidator : OrganizationValidator<ChangeOrgPassword>
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
            RuleFor(it => it.ConfirmPassword).NotNull().WithMessage("确认密码不能为空");
            RuleFor(it => it.ConfirmPassword).Equal(it => it.NewPassword).WithMessage("确认密码不一致");
        }
        public ChangeOrgPasswordValidator()
        {
            ValidateOrgIdentifier();
            ValidateOldPassword();
            ValidateNewPassword();
            ValidateConfirmPassword();
        }
    }
}