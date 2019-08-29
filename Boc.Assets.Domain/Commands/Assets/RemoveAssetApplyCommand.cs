﻿using Boc.Assets.Domain.Commands.Validations.Assets;
using Boc.Assets.Domain.Core.SharedKernel;
using System;

namespace Boc.Assets.Domain.Commands.Assets
{
    public class RemoveAssetApplyCommand : AssetCommand
    {
        public RemoveAssetApplyCommand(IUser principal, Guid eventId) : base(principal)
        {
            EventId = eventId;
        }

        public override bool IsValid()
        {
            ValidationResult = new RemoveAssetApplyCommandValidator().Validate(this);
            return ValidationResult.IsValid;
        }
    }
}