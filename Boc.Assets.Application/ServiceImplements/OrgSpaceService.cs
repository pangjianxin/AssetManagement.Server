using AutoMapper;
using Boc.Assets.Application.Dto;
using Boc.Assets.Application.ServiceInterfaces;
using Boc.Assets.Application.ViewModels.OrganizationSpace;
using Boc.Assets.Domain.Commands.OrganizationSpace;
using Boc.Assets.Domain.Core.Bus;
using Boc.Assets.Domain.Core.SharedKernel;
using Boc.Assets.Domain.Models.Organizations;
using Boc.Assets.Domain.Repositories;
using System;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace Boc.Assets.Application.ServiceImplements
{
    public class OrgSpaceService : IOrgSpaceService
    {
        private readonly IMapper _mapper;
        private readonly IOrgSpaceRepository _spaceRepository;
        private readonly IBus _bus;
        private readonly IUser _user;

        public OrgSpaceService(IMapper mapper,
            IOrgSpaceRepository spaceRepository,
            IBus bus,
            IUser user)
        {
            _mapper = mapper;
            _spaceRepository = spaceRepository;
            _bus = bus;
            _user = user;
        }

        public async Task CreateAsync(CreateSpace model)
        {
            var command = _mapper.Map<CreateSpaceCommand>(model);
            await _bus.SendCommandAsync(command);
        }

        public IQueryable<OrgSpaceDto> Get(Expression<Func<OrganizationSpace, bool>> predicate = null)
        {
            return _mapper.ProjectTo<OrgSpaceDto>(_spaceRepository.GetAll(predicate));
        }

        public async Task ModifyAsync(ModifySpaceInfo model)
        {
            var command = _mapper.Map<ModifySpaceInfoCommand>(model);
            await _bus.SendCommandAsync(command);
        }
    }
}