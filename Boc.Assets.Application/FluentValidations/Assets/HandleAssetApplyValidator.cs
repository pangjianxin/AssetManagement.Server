using Boc.Assets.Application.ViewModels.Assets;

namespace Boc.Assets.Application.FluentValidations.Assets
{
    public class HandleAssetApplyValidator : AssetValidator<HandleAssetApply>
    {
        public HandleAssetApplyValidator()
        {
            ValidateEventId();
            ValidateAssetId();
            //RuleFor(it => it.EventId).NotNull().NotEmpty().WithMessage("事件ID不能为空,请重新输入")
            //    .NotEqual(Guid.Empty).WithMessage("事件ID不能为空,请重新输入");
            //RuleFor(it => it.AssetId).NotNull().NotEmpty().WithMessage("资产ID不能为空,请重新输入")
            //    .NotEqual(Guid.Empty).WithMessage("资产ID不能为空,请重新输入");
        }
    }
}