using AutoMapper;
using Boc.Assets.Application.Dto;
using Boc.Assets.Application.ServiceInterfaces;
using Boc.Assets.Domain.Core.Bus;
using Boc.Assets.Domain.Repositories;
using System.Linq;

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

        public IQueryable<OrganizationRoleDto> Get(int role)
        {
            return _mapper.ProjectTo<OrganizationRoleDto>(_orgRoleRepository.GetAll(it => (int)it.Role == role));
        }

        public IQueryable<OrganizationRoleDto> Get()
        {
            return _mapper.ProjectTo<OrganizationRoleDto>(_orgRoleRepository.GetAll());
        }
    }
}