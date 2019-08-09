using AutoMapper;
using AutoMapper.QueryableExtensions;
using Boc.Assets.Application.Dto;
using Boc.Assets.Application.ServiceInterfaces;
using Boc.Assets.Domain.Core.Bus;
using Boc.Assets.Domain.Repositories;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Boc.Assets.Application.ServiceImplements
{
    public class OrganizationRoleService : IOrganizationRoleService
    {
        private readonly IOrgRoleRepository _orgRoleRepository;
        private readonly IMapper _mapper;
        private readonly IBus _bus;
        public OrganizationRoleService(IOrgRoleRepository orgRoleRepository,
            IMapper mapper,
            IBus bus)
        {
            _orgRoleRepository = orgRoleRepository;
            _mapper = mapper;
            _bus = bus;
        }

        public async Task<IEnumerable<OrganizationRoleDto>> GetAll(int role)
        {
            var querys = _orgRoleRepository.GetAll(it => (int)it.Role <= role).ProjectTo<OrganizationRoleDto>(_mapper.ConfigurationProvider);
            return await querys.ToListAsync();
        }
    }
}