using Boc.Assets.Domain.Core.Models;
using System.Collections.Generic;

namespace Boc.Assets.Domain.Models.Organizations
{
    public class OrganizationRole : EntityBase
    {
        public OrganizationRole()
        {
        }
        public Role Role { get; set; }
        public string Description { get; set; }

        public virtual ICollection<Organization> Organizations { get; set; }
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