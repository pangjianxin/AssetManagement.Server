using Boc.Assets.Domain.Commands.Permissions;
using FluentValidation;
using System;
using System.Linq;
namespace Boc.Assets.Domain.Validations.Permissions
{
    public class ModifyPermissionCommandValidator : AbstractValidator<ModifyPermissionCommand>
    {
        public ModifyPermissionCommandValidator()
        {
            RuleFor(it => it.RoleId).NotEqual(Guid.Empty).WithMessage("角色ID不能为空");
            RuleFor(it => it.Permissions).Must(it => it.Where(value =>
                                    !string.IsNullOrEmpty(value.Controller) && !string.IsNullOrEmpty(value.Action))
                                .Count() > 0).WithMessage("权限信息不能为空");
        }
    }
}