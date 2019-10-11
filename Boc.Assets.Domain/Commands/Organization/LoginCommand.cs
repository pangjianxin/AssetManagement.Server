using Boc.Assets.Domain.Commands.Validations.Organization;
using Boc.Assets.Domain.Core.Commands;

namespace Boc.Assets.Domain.Commands.Organization
{
    public class LoginCommand : Command<string>
    {
        public string OrgIdentifier { get; }
        public string Password { get; }
        public LoginCommand(string identifier, string password)
        {
            OrgIdentifier = identifier;
            Password = password;
        }
        public override bool IsValid()
        {
            ValidationResult = new LoginCommandValidator().Validate(this);
            return ValidationResult.IsValid;
        }
    }
}