using Boc.Assets.Domain.Core.Models;
using Microsoft.EntityFrameworkCore.Infrastructure;
using System.Collections.Generic;

namespace Boc.Assets.Domain.Models.Organizations
{
    public class OrganizationRole : EntityBase
    {
        private readonly ILazyLoader _lazyLoader;

        public OrganizationRole(ILazyLoader lazyLoader)
        {
            _lazyLoader = lazyLoader;
        }
        public Role Role { get; set; }
        public string Description { get; set; }
        private ICollection<Organization> _organizations;

        public ICollection<Organization> Organizations
        {
            get => _lazyLoader.Load(this, ref _organizations);
            set => _organizations = value;
        }
        private ICollection<Permission> _permissions;
        public ICollection<Permission> Permissions
        {
            get => _lazyLoader.Load(this, ref _permissions);
            set => _permissions = value;
        }
        public override string ToString()
        {
            return Role.ToString();
        }
    }

    public enum Role : int
    {
        普通用户 = 1,
        二级行管理员 = 2,
        系统管理员 = 3
    }
}