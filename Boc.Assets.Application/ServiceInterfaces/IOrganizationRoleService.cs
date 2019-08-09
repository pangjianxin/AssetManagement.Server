using Boc.Assets.Application.Dto;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Boc.Assets.Application.ServiceInterfaces
{
    public interface IOrganizationRoleService : IApplicationService
    {
        Task<IEnumerable<OrganizationRoleDto>> GetAll(int role);
    }
}