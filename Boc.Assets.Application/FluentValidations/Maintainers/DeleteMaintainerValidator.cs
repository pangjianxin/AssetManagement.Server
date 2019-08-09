using Boc.Assets.Application.ViewModels.Maintainers;

namespace Boc.Assets.Application.FluentValidations.Maintainers
{
    public class DeleteMaintainerValidator : MaintainersValidator<DeleteMaintainer>
    {
        public DeleteMaintainerValidator()
        {
            ValidateMaintainerId();
        }
    }
}