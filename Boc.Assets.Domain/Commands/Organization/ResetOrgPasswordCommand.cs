using Boc.Assets.Domain.Commands.Validations.Organization;
using Boc.Assets.Domain.Core.Commands;

namespace Boc.Assets.Domain.Commands.Organization
{
    public class ResetOrgPasswordCommand : Command<bool>
    {
        public string OrgIdentifier { get; }
        public ResetOrgPasswordCommand(string orgIdentifier)
        {
            OrgIdentifier = orgIdentifier;
        }
        public override bool IsValid()
        {
            ValidationResult = new ResetOrgPasswordCommandValidator().Validate(this);
            return ValidationResult.IsValid;
        }
    }
}