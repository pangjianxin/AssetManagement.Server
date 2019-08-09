using AutoMapper;
using AutoMapper.QueryableExtensions;
using Boc.Assets.Application.Dto;
using Boc.Assets.Application.Pagination;
using Boc.Assets.Application.ServiceInterfaces;
using Boc.Assets.Application.Sieve.Models;
using Boc.Assets.Application.Sieve.Services;
using Boc.Assets.Application.ViewModels.OrganizationSpace;
using Boc.Assets.Domain.Commands.OrganizationSpace;
using Boc.Assets.Domain.Core.Bus;
using Boc.Assets.Domain.Core.SharedKernel;
using Boc.Assets.Domain.Repositories;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Boc.Assets.Application.ServiceImplements
{
    public class OrgSpaceService : IOrgSpaceService
    {
        private readonly IMapper _mapper;
        private readonly IOrgSpaceRepository _spaceRepository;
        private readonly IBus _bus;
        private readonly IUser _user;
        private readonly ISieveProcessor _sieveProcessor;
        private readonly SieveOptions _sieveOptions;

        public OrgSpaceService(IMapper mapper,
            IOrgSpaceRepository spaceRepository,
            IBus bus,
            IUser user,
            ISieveProcessor sieveProcessor,
            IOptions<SieveOptions> options)
        {
            _mapper = mapper;
            _spaceRepository = spaceRepository;
            _bus = bus;
            _user = user;
            _sieveProcessor = sieveProcessor;
            _sieveOptions = options.Value;
        }

        public async Task CreateAsync(CreateSpace model)
        {
            var command = _mapper.Map<CreateSpaceCommand>(model);
            await _bus.SendCommandAsync(command);
        }

        public async Task<List<OrgSpaceDto>> GetAllListAsync()
        {
            var queryResult = await _spaceRepository.GetAll(it => it.OrgId == _user.OrgId).ProjectTo<OrgSpaceDto>(_mapper.ConfigurationProvider).ToListAsync();
            return queryResult;
        }

        public async Task ModifyAsync(ModifySpaceInfo model)
        {
            var command = _mapper.Map<ModifySpaceInfoCommand>(model);
            await _bus.SendCommandAsync(command);
        }

        public async Task<PaginatedList<OrgSpaceDto>> Pagination(SieveModel model)
        {
            var entities = _spaceRepository.GetAll(it => it.OrgId == _user.OrgId);
            entities = _sieveProcessor.Apply(model, entities, applyPagination: false);
            var count = entities.Count();
            var result = await _sieveProcessor.Apply(model, entities).ProjectTo<OrgSpaceDto>(_mapper.ConfigurationProvider).ToListAsync();
            return new PaginatedList<OrgSpaceDto>(_sieveOptions, model.Page, model.PageSize, count, result);
        }
    }
}