using AutoMapper;
using Boc.Assets.Application.Dto;
using Boc.Assets.Application.ServiceInterfaces;
using Boc.Assets.Application.ViewModels.Assets;
using Boc.Assets.Domain.Commands.Assets;
using Boc.Assets.Domain.Core.Bus;
using Boc.Assets.Domain.Models.Assets;
using Boc.Assets.Domain.Repositories;
using System;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace Boc.Assets.Application.ServiceImplements
{
    public class AssetService : IAssetService
    {
        private readonly IMapper _mapper;
        private readonly IAssetRepository _assetRepository;
        private readonly IBus _bus;

        public AssetService(IMapper mapper,
            IAssetRepository assetRepository,
            IBus bus)
        {
            _mapper = mapper;
            _assetRepository = assetRepository;
            _bus = bus;
        }
        public async Task ModifyAssetLocationAsync(ModifyAssetLocation model)
        {
            var command = _mapper.Map<ModifyAssetLocationCommand>(model);
            await _bus.SendCommandAsync(command);
        }

        public async Task StorageWithOutFile(StoreAsset model)
        {
            var command = _mapper.Map<StoreAssetWithOutFileCommand>(model);
            await _bus.SendCommandAsync(command);
        }
        public IQueryable<AssetDto> Get(Expression<Func<Asset, bool>> predicate = null)
        {
            return _mapper.ProjectTo<AssetDto>(_assetRepository.GetAll(predicate));
            //return _assetRepository.GetAll(predicate)
            //    .UseAsDataSource(_mapper.ConfigurationProvider).For<AssetDto>();
        }
    }
}