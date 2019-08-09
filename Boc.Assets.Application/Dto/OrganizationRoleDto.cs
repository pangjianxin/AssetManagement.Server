using System;

namespace Boc.Assets.Application.Dto
{
    public class OrganizationRoleDto
    {
        public Guid Id { get; set; }
        public string RoleNam { get; set; }
        public int RoleEnum { get; set; }
        public string Description { get; set; }
    }
}