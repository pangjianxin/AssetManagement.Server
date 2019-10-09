using Boc.Assets.Domain.Commands.Assets;
using FluentValidation;
using System;

namespace Boc.Assets.Domain.Commands.Validations.Assets
{
    public class RemoveAssetExchangeCommandValidator : ApplyCommandValidator<RemoveAssetExchangeCommand>
    {
        public RemoveAssetExchangeCommandValidator()
        {
            ValidateApplyId();
        }
    }
}