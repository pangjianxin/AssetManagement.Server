using Boc.Assets.Application.ViewModels.OrganizationSpace;
using FluentValidation;

namespace Boc.Assets.Application.FluentValidations.OrganizationSpace
{
    public abstract class OrgSpaceValidator<TViewModel>
        : AbstractValidator<TViewModel> where TViewModel : OrganizationSpaceViewModel
    {
        protected void ValidateSpaceId()
        {
            RuleFor(it => it.SpaceId).NotNull().NotEmpty().WithMessage("空间ID不能为空");
        }
        protected void ValidateSpaceName()
        {
            RuleFor(it => it.SpaceName).NotNull().NotEmpty().WithMessage("空间名称不能为空");
        }
        protected void ValidateSpaceDescription()
        {
            RuleFor(it => it.SpaceDescription).NotNull().NotEmpty().WithMessage("空间描述不能为空");
        }
    }
}