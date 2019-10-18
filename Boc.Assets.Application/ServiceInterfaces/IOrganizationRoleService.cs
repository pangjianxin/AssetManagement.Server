using Boc.Assets.Application.Dto;
using System.Linq;

namespace Boc.Assets.Application.ServiceInterfaces
{
    public interface IOrganizationRoleService : IApplicationService
    {
        IQueryable<OrganizationRoleDto> Get(int role);
        IQueryable<OrganizationRoleDto> Get();
    }
}