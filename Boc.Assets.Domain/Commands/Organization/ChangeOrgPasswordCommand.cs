using Boc.Assets.Domain.Commands.Validations.Organization;
using Boc.Assets.Domain.Core.SharedKernel;

namespace Boc.Assets.Domain.Commands.Organization
{
    public class ChangeOrgPasswordCommand : OrganizationCommand
    {

        public ChangeOrgPasswordCommand(IUser principal,
            string orgIdentifier,
            string oldPassword,
            string newPassword,
            string confirmPassword) : base(principal)
        {
            OrgIdentifier = orgIdentifier;
            OldPassword = oldPassword;
            NewPassword = newPassword;
            ConfirmPassword = confirmPassword;
        }
        public string OldPassword { get; set; }
        public string NewPassword { get; set; }
        public string ConfirmPassword { get; set; }

        public override bool IsValid()
        {
            ValidationResult = new ChangeOrgPasswordCommandValidator().Validate(this);
            return ValidationResult.IsValid;
        }
    }
}