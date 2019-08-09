using Boc.Assets.Domain.Commands.Assets;
using FluentValidation;
using System;

namespace Boc.Assets.Domain.Validations.Assets
{
    public class HandleAssetReturnCommandValidator : AssetCommandValidator<HandleAssetReturnCommand>
    {
        public HandleAssetReturnCommandValidator()
        {
            ValidateEventId();
            ValidatePrincipal();
        }
    }
}