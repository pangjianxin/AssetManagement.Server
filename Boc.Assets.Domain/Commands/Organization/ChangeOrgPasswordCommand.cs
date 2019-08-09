using Boc.Assets.Domain.Core.SharedKernel;
using Boc.Assets.Domain.Validations.Organization;
using System.Threading.Tasks;

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

        public override async Task<bool> IsValid()
        {
            ValidationResult = await new ChangeOrgPasswordCommandValidator().ValidateAsync(this);
            return ValidationResult.IsValid;
        }
    }
}