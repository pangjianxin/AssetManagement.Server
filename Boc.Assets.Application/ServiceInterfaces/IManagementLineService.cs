using Boc.Assets.Application.Dto;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Boc.Assets.Application.ServiceInterfaces
{
    public interface IManagementLineService : IApplicationService
    {
        Task<IEnumerable<OrgDto>> GetTargetExaminations(Guid managementLineId);
    }
}