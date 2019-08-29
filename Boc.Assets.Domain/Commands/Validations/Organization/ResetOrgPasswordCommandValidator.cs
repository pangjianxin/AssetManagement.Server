using Boc.Assets.Domain.Commands.Organization;

namespace Boc.Assets.Domain.Commands.Validations.Organization
{
    public class ResetOrgPasswordCommandValidator : OrganizationCommandValidator<ResetOrgPasswordCommand>
    {

        public ResetOrgPasswordCommandValidator()
        {
            ValidateOrgIdentifier();
            ValidatePrincipal();
        }
    }
}