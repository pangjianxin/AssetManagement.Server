﻿using Boc.Assets.Domain.Commands.Assets;

namespace Boc.Assets.Domain.Commands.Validations.Assets
{
    public class RemoveAssetReturnCommandValidator : AssetCommandValidator<RemoveAssetReturnCommand>
    {
        public RemoveAssetReturnCommandValidator()
        {
            ValidateEventId();
        }
    }
}