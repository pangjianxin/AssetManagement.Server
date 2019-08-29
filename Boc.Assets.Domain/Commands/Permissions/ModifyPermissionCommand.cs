using Boc.Assets.Domain.Commands.Validations.Permissions;
using Boc.Assets.Domain.Core.Commands;
using System;

namespace Boc.Assets.Domain.Commands.Permissions
{
    public class ModifyPermissionCommand : Command
    {
        public Guid RoleId { get; set; }
        public PermissionModel[] Permissions { get; set; }
        public override bool IsValid()
        {
            ValidationResult = new ModifyPermissionCommandValidator().Validate(this);
            return ValidationResult.IsValid;
        }
    }
    public class PermissionModel
    {
        public string Controller { get; set; }
        public string Action { get; set; }
    }
}