using Boc.Assets.Domain.Core.Models;
using Microsoft.EntityFrameworkCore.Infrastructure;
using System;

namespace Boc.Assets.Domain.Models.Organizations
{
    public class Permission : EntityBase
    {
        private readonly ILazyLoader _lazyLoader;

        public Permission(ILazyLoader lazyLoader = null)
        {
            _lazyLoader = lazyLoader;
        }
        /// <summary>
        /// 角色ID
        /// </summary>
        public Guid RoleId { get; set; }
        /// <summary>
        /// 控制器的名称
        /// </summary>
        public string ControllerName { get; set; }
        /// <summary>
        /// 动作名称
        /// </summary>
        public string ActionName { get; set; }
        /// <summary>
        /// 权限名称
        /// </summary>
        public string PermissionName => $"{ControllerName}.{ActionName}";

        private OrganizationRole _organizationRole;

        public OrganizationRole OrganizationRole
        {
            get => _lazyLoader.Load(this, ref _organizationRole);
            set => _organizationRole = value;
        }
    }
}