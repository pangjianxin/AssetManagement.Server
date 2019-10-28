using System;
using System.ComponentModel.DataAnnotations;

namespace Boc.Assets.Application.Dto
{
    public class OrganizationRoleDto
    {
        [Key]
        public Guid Id { get; set; }
        public string RoleNam { get; set; }
        public int RoleEnum { get; set; }
        public string Description { get; set; }
    }
}