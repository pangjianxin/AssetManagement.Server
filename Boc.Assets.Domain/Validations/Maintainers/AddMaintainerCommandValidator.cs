using Boc.Assets.Domain.Commands.Maintainers;

namespace Boc.Assets.Domain.Validations.Maintainers
{
    public class AddMaintainerCommandValidator : MaintainerCommandValidator<AddMaintainerCommand>
    {
        public AddMaintainerCommandValidator()
        {
            ValidateCategoryId();
            ValidateCompanyName();
            ValidateMaintainerName();
            ValidateTelephone();
            ValidatePrincipal();
        }
    }
}