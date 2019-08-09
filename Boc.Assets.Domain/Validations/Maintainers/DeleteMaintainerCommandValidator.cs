using Boc.Assets.Domain.Commands.Maintainers;

namespace Boc.Assets.Domain.Validations.Maintainers
{
    public class DeleteMaintainerCommandValidator : MaintainerCommandValidator<DeleteMaintainerCommand>
    {
        public DeleteMaintainerCommandValidator()
        {
            ValidateMaintainerId();
            ValidatePrincipal();
        }
    }
}