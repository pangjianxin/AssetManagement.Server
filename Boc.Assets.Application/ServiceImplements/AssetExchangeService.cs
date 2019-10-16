using AutoMapper;
using AutoMapper.QueryableExtensions;
using Boc.Assets.Application.Dto;
using Boc.Assets.Application.Pagination;
using Boc.Assets.Application.ServiceInterfaces;
using Boc.Assets.Application.ViewModels.Assets;
using Boc.Assets.Domain.Commands.Assets;
using Boc.Assets.Domain.Core.Bus;
using Boc.Assets.Domain.Models.Applies;
using Boc.Assets.Domain.Repositories;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
using System;
using System.Linq.Expressions;
using System.Threading.Tasks;
using Sieve.Models;
using Sieve.Services;

namespace Boc.Assets.Application.ServiceImplements
{
    public class AssetExchangeService : IAssetExchangeService
    {
        private readonly IAssetExchangeRepository _assetExchangeRepository;
        private readonly IMapper _mapper;
        private readonly IBus _bus;
        private readonly ISieveProcessor _sieveProcessor;
        private readonly SieveOptions _sieveOptions;

        public AssetExchangeService(IAssetExchangeRepository assetExchangeRepository,
            IMapper mapper,
            IBus bus,
            ISieveProcessor sievingProcessor,
            IOptions<SieveOptions> options)
        {
            _assetExchangeRepository = assetExchangeRepository;
            _mapper = mapper;
            _bus = bus;
            _sieveProcessor = sievingProcessor;
            _sieveOptions = options.Value;
        }


        public async Task<PaginatedList<AssetExchangeDto>> PaginationAsync(SieveModel model, Expression<Func<AssetExchange, bool>> predicate)
        {
            var entities = _assetExchangeRepository.GetAll(predicate);
            var count = await _sieveProcessor.Apply(model, entities, applyPagination: false).CountAsync();
            var result = _sieveProcessor.Apply(model, entities).ProjectTo<AssetExchangeDto>(_mapper.ConfigurationProvider);
            var paggedList = await result.ToListAsync();
            return new PaginatedList<AssetExchangeDto>(_sieveOptions, model.Page, model.PageSize, count, paggedList);
        }
        public async Task<bool> RemoveAssetExchangeAsync(RemoveAssetExchange model)
        {
            var command = _mapper.Map<RemoveAssetExchangeCommand>(model);
            return await _bus.SendCommandAsync(command);
        }

        public async Task HandleAssetExchangeAsync(HandleAssetExchange model)
        {
            var command = _mapper.Map<HandleAssetExchangeCommand>(model);
            await _bus.SendCommandAsync(command);
        }

        public async Task RevokeAssetExchangeAsync(RevokeAssetExchange model)
        {
            var command = _mapper.Map<RevokeAssetExchangeCommand>(model);
            await _bus.SendCommandAsync(command);
        }

        public async Task CreateAssetExchangeAsync(ExchangeAsset model)
        {
            var command = _mapper.Map<CreateAssetExchangeCommand>(model);
            await _bus.SendCommandAsync(command);
        }

    }
}