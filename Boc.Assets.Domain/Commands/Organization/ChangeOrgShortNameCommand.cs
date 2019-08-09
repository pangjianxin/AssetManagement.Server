using Boc.Assets.Domain.Core.SharedKernel;
using Boc.Assets.Domain.Validations.Organization;
using System.Threading.Tasks;

namespace Boc.Assets.Domain.Commands.Organization
{
    public class ChangeOrgShortNameCommand : OrganizationCommand
    {
        public string OrgShortNam { get; set; }
        public ChangeOrgShortNameCommand(IUser principal, string orgIdentifier, string orgShortNam) : base(principal)
        {
            OrgIdentifier = orgIdentifier;
            OrgShortNam = orgShortNam;
        }
        public override async Task<bool> IsValid()
        {
            ValidationResult = await new ChangeOrgShortNameCommandValidator().ValidateAsync(this);
            return ValidationResult.IsValid;
        }
    }
}