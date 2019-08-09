using Boc.Assets.Application.ViewModels.Maintainers;

namespace Boc.Assets.Application.FluentValidations.Maintainers
{
    public class AddMaintainerValidator : MaintainersValidator<AddMaintainer>
    {
        public AddMaintainerValidator()
        {
            ValidateCompanyName();
            ValidateMaintainerName();
            ValidateCategoryId();
            ValidateTelephone();
        }
    }
}