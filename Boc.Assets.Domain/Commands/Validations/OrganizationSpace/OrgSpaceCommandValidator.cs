using Boc.Assets.Domain.Commands.OrganizationSpace;
using FluentValidation;

namespace Boc.Assets.Domain.Commands.Validations.OrganizationSpace
{
    public class OrgSpaceCommandValidator<TCommand> : AbstractValidator<TCommand> where TCommand : OrgSpaceCommand
    {
        protected void ValidateSpaceId()
        {
            RuleFor(it => it.SpaceId).NotNull().NotEmpty().WithMessage("空间Id不能为空");
        }
        protected void ValidateSpaceName()
        {
            RuleFor(it => it.SpaceName).NotNull().NotEmpty().WithMessage("空间名称不能为空");
        }
        protected void ValidateSpaceDescription()
        {
            RuleFor(it => it.SpaceDescription).NotNull().NotEmpty().WithMessage("空间描述不能为空");
        }
    }
}