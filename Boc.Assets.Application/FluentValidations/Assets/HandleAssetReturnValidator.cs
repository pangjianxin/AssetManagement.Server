using Boc.Assets.Application.ViewModels.Assets;

namespace Boc.Assets.Application.FluentValidations.Assets
{
    public class HandleAssetReturnValidator : AssetValidator<HandleAssetReturn>
    {
        public HandleAssetReturnValidator()
        {
            ValidateEventId();
            //RuleFor(it => it.EventId).NotNull().NotEmpty().WithMessage("事件ID不能为空,请重新输入")
            //    .NotEqual(Guid.Empty).WithMessage("事件ID不能为空,请重新输入");
        }
    }
}