using Boc.Assets.Domain.Commands.Validations.Organization;
using Boc.Assets.Domain.Core.Commands;

namespace Boc.Assets.Domain.Commands.Organization
{
    public class ChangeOrgPasswordCommand : Command<string>
    {
        public string OrgIdentifier { get; }
        public string OldPassword { get; }
        public string NewPassword { get; }
        public string ConfirmPassword { get; }
        public ChangeOrgPasswordCommand(
            string orgIdentifier,
            string oldPassword,
            string newPassword,
            string confirmPassword)
        {
            OrgIdentifier = orgIdentifier;
            OldPassword = oldPassword;
            NewPassword = newPassword;
            ConfirmPassword = confirmPassword;
        }
        public override bool IsValid()
        {
            ValidationResult = new ChangeOrgPasswordCommandValidator().Validate(this);
            return ValidationResult.IsValid;
        }
    }
}