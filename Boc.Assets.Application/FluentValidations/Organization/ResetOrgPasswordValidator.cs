using Boc.Assets.Application.ViewModels.Organization;

namespace Boc.Assets.Application.FluentValidations.Organization
{
    public class ResetOrgPasswordValidator : OrganizationValidator<ResetOrgPassword>
    {
        public ResetOrgPasswordValidator()
        {
            ValidateOrgIdentifier();
        }
    }
}