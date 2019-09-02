using Boc.Assets.Domain.Commands.Maintainers;

namespace Boc.Assets.Domain.Commands.Validations.Maintainers
{
    public class AddMaintainerCommandValidator : MaintainerCommandValidator<AddMaintainerCommand>
    {
        public AddMaintainerCommandValidator()
        {
            ValidateOrganizationId();
            ValidateOrg2();
            ValidateCategoryId();
            ValidateCompanyName();
            ValidateMaintainerName();
            ValidateTelephone();
        }
    }
}