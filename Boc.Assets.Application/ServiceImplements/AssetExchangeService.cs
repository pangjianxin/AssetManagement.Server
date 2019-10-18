using AutoMapper;
using Boc.Assets.Application.Dto;
using Boc.Assets.Application.ServiceInterfaces;
using Boc.Assets.Application.ViewModels.Assets;
using Boc.Assets.Domain.Commands.Assets;
using Boc.Assets.Domain.Core.Bus;
using Boc.Assets.Domain.Models.Applies;
using Boc.Assets.Domain.Repositories;
using System;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace Boc.Assets.Application.ServiceImplements
{
    public class AssetExchangeService : IAssetExchangeService
    {
        private readonly IAssetExchangeRepository _assetExchangeRepository;
        private readonly IMapper _mapper;
        private readonly IBus _bus;

        public AssetExchangeService(IAssetExchangeRepository assetExchangeRepository,
            IMapper mapper,
            IBus bus)
        {
            _assetExchangeRepository = assetExchangeRepository;
            _mapper = mapper;
            _bus = bus;
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

        public IQueryable<AssetExchangeDto> Get(Expression<Func<AssetExchange, bool>> predicate = null)
        {
            return _mapper.ProjectTo<AssetExchangeDto>(_assetExchangeRepository.GetAll(predicate));
        }
    }
}