using Boc.Assets.Application.ViewModels.Permission;
using FluentValidation;

namespace Boc.Assets.Application.FluentValidations.Permission
{
    public class PermissionValidator<TViewModel> : AbstractValidator<TViewModel> where TViewModel : PermissionViewModel
    {
        protected void ValidateController()
        {
            RuleFor(it => it.Controller).NotNull().NotEmpty().WithMessage("控制器名称不能为空");
        }

        protected void ValidateAction()
        {
            RuleFor(it => it.Action).NotNull().NotEmpty().WithMessage("操作名称不能为空");
        }
    }
}