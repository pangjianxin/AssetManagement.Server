﻿using Boc.Assets.Application.ViewModels.Assets;

namespace Boc.Assets.Application.FluentValidations.Assets
{
    public class RemoveAssetReturnValidator : AssetValidator<RemoveAssetReturn>
    {
        public RemoveAssetReturnValidator()
        {
            ValidateEventId();
        }
    }
}