using System;

namespace Boc.Assets.Application.ViewModels.Permission
{
    public class ModifyPermission : ViewModel
    {
        public Guid RoleId { get; set; }
        public PermissionViewModel[] Permissions { get; set; }
    }
}