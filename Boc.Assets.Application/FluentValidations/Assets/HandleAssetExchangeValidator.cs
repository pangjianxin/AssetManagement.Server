using Boc.Assets.Application.ViewModels.Assets;
using FluentValidation;
using System;

namespace Boc.Assets.Application.FluentValidations.Assets
{
    public class HandleAssetExchangeValidator : AssetValidator<HandleAssetExchange>
    {
        public HandleAssetExchangeValidator()
        {
            ValidateEventId();
        }
    }
}