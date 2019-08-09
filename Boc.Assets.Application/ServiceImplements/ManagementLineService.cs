using AutoMapper;
using Boc.Assets.Application.Dto;
using Boc.Assets.Application.ServiceInterfaces;
using Boc.Assets.Domain.Core.SharedKernel;
using Boc.Assets.Domain.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Boc.Assets.Application.Pagination
{
    public class ManagementLineService : IManagementLineService
    {
        private readonly IManagementLineRepository _managementLineRepository;
        private readonly IMapper _mapper;
        private readonly IUser _user;

        public ManagementLineService(IManagementLineRepository managementLineRepository,
            IMapper mapper,
            IUser user)
        {
            _managementLineRepository = managementLineRepository;
            _mapper = mapper;
            _user = user;
        }
        public async Task<IEnumerable<OrgDto>> GetTargetExaminations(Guid managementLineId)
        {
            var managementLine = await _managementLineRepository.GetByIdAsync(managementLineId);
            var orgs = managementLine.Organizations.Where(it => it.Org2 == _user.Org2);
            return _mapper.Map<IEnumerable<OrgDto>>(orgs);
        }
    }
}