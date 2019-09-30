using Boc.Assets.Application.Dto;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Boc.Assets.Application.ServiceInterfaces
{
    public interface ICategoryManageResgisterService : IApplicationService
    {
        Task<IEnumerable<OrgDto>> GetOrgByCategoryAndOrg2(Guid categoryId, string org2);
    }
}