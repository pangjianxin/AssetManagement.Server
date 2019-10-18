using AutoMapper;
using Boc.Assets.Application.Dto;
using Boc.Assets.Application.ServiceInterfaces;
using Boc.Assets.Application.ViewModels.Maintainers;
using Boc.Assets.Domain.Commands.Maintainers;
using Boc.Assets.Domain.Core.Bus;
using Boc.Assets.Domain.Core.SharedKernel;
using Boc.Assets.Domain.Models.Assets;
using Boc.Assets.Domain.Repositories;
using System;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace Boc.Assets.Application.ServiceImplements
{
    public class MaintainerService : IMaintainerService
    {
        private readonly IBus _bus;
        private readonly IMapper _mapper;
        private readonly IMaintainerRepository _maintainerRepository;
        private readonly IUser _user;

        public MaintainerService(
            IBus bus,
            IMapper mapper,
            IMaintainerRepository maintainerRepository,
            IUser user)
        {
            _bus = bus;
            _mapper = mapper;
            _maintainerRepository = maintainerRepository;
            _user = user;
        }
        public async Task AddMaintainerAsync(AddMaintainer model)
        {
            model.OrganizationId = _user.OrgId;
            model.Org2 = _user.Org2;
            var command = _mapper.Map<AddMaintainerCommand>(model);
            await _bus.SendCommandAsync(command);
        }
        public async Task DeleteAsync(DeleteMaintainer model)
        {
            var command = _mapper.Map<DeleteMaintainerCommand>(model);
            await _bus.SendCommandAsync(command);
        }

        public IQueryable<MaintainerDto> Get(Expression<Func<Maintainer, bool>> predicate)
        {
            return _mapper.ProjectTo<MaintainerDto>(_maintainerRepository.GetAll(predicate));
        }
    }
}