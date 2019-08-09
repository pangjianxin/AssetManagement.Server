using Boc.Assets.Domain.Core.Commands;
using Boc.Assets.Domain.Core.SharedKernel;
using Boc.Assets.Domain.Validations.Permissions;
using System;
using System.Threading.Tasks;

namespace Boc.Assets.Domain.Commands.Permissions
{
    public class ModifyPermissionCommand : Command
    {
        public Guid RoleId { get; set; }
        public PermissionModel[] Permissions { get; set; }
        public ModifyPermissionCommand(IUser principal) : base(principal)
        {
        }
        public override async Task<bool> IsValid()
        {
            ValidationResult = await new ModifyPermissionCommandValidator().ValidateAsync(this);
            return ValidationResult.IsValid;
        }
    }
    public class PermissionModel
    {
        public string Controller { get; set; }
        public string Action { get; set; }
    }
}