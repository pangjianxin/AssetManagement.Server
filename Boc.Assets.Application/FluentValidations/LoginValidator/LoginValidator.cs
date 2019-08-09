using Boc.Assets.Application.ViewModels.Login;
using FluentValidation;

namespace Boc.Assets.Application.FluentValidations.LoginValidator
{
    public class LoginValidator : AbstractValidator<Login>
    {
        public LoginValidator()
        {
            RuleFor(it => it.OrgIdentifier).NotNull().NotEmpty().WithMessage("机构号不能为空");
            RuleFor(it => it.Password).NotNull().NotEmpty().WithMessage("密码不能为空");
        }
    }
}