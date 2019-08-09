using Boc.Assets.Domain.Commands.Organization;
using FluentValidation;

namespace Boc.Assets.Domain.Validations.Organization
{
    public abstract class OrganizationCommandValidator<TCommand> : AbstractValidator<TCommand>
        where TCommand : OrganizationCommand
    {

        protected void ValidateOrgIdentifier()
        {
            RuleFor(it => it.OrgIdentifier).NotNull().NotEmpty().WithMessage("机构号不能为空");
        }

        protected void ValidateOrgNam()
        {
            RuleFor(it => it.OrgNam).NotNull().NotEmpty().WithMessage("机构名称不能为空");
        }
        protected void ValidatePrincipal()
        {
            RuleFor(it => it.Principal).NotNull().WithMessage("主体不能为空");
        }
    }
}