using AutoMapper;
using Boc.Assets.Application.Dto;
using Boc.Assets.Application.ServiceInterfaces;
using Boc.Assets.Application.ViewModels.AssetInventories;
using Boc.Assets.Domain.Commands.AssetInventory;
using Boc.Assets.Domain.Core.Bus;
using Boc.Assets.Domain.Models.AssetInventories;
using Boc.Assets.Domain.Models.Assets;
using Boc.Assets.Domain.Repositories;
using System;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace Boc.Assets.Application.ServiceImplements
{
    public class AssetInventoryService : IAssetInventoryService
    {
        private readonly IMapper _mapper;
        private readonly IBus _bus;
        private readonly IAssetInventoryRepository _assetInventoryRepository;
        private readonly IAssetInventoryRegisterRepository _inventoryRegisterRepository;
        private readonly IAssetRepository _assetRepository;
        private readonly IAssetInventoryDetailRepository _assetInventoryDetailRepository;

        public AssetInventoryService(
            IMapper mapper,
            IBus bus,
            IAssetInventoryRepository assetInventoryRepository,
            IAssetInventoryRegisterRepository inventoryRegisterRepository,
            IAssetRepository assetRepository,
            IAssetInventoryDetailRepository assetInventoryDetailRepository)
        {
            _mapper = mapper;
            _bus = bus;
            _assetInventoryRepository = assetInventoryRepository;
            _inventoryRegisterRepository = inventoryRegisterRepository;
            _assetRepository = assetRepository;
            _assetInventoryDetailRepository = assetInventoryDetailRepository;
        }
        /// <summary>
        /// 创建资产盘点
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        public async Task CreateAssetInventoryAsync(CreateAssetInventory model)
        {
            var createAssetStockTakingCommand = _mapper.Map<CreateAssetInventoryCommand>(model);
            await _bus.SendCommandAsync(createAssetStockTakingCommand);
        }
        /// <summary>
        /// 创建资产盘点明细
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        public async Task CreatAssetInventoryDetailAsync(CreateAssetInventoryDetail model)
        {
            var command = _mapper.Map<CreateAssetInventoryDetailCommand>(model);
            await _bus.SendCommandAsync(command);
        }
        /// <summary>
        /// 检索资产盘点数据
        /// </summary>
        /// <param name="predicate"></param>
        /// <returns></returns>
        public IQueryable<AssetInventoryDto> GetInventories(Expression<Func<AssetInventory, bool>> predicate = null)
        {
            return _mapper.ProjectTo<AssetInventoryDto>(_assetInventoryRepository.GetAll(predicate));
        }
        /// <summary>
        /// 检索参与机构数据
        /// </summary>
        /// <param name="predicate"></param>
        /// <returns></returns>
        public IQueryable<AssetInventoryResgiterDto> GetInventoryRegisters(Expression<Func<AssetInventoryRegister, bool>> predicate = null)
        {
            return _mapper.ProjectTo<AssetInventoryResgiterDto>(_inventoryRegisterRepository.GetAll(predicate));
        }
        /// <summary>
        /// 检索资产盘点明细
        /// </summary>
        /// <param name="predicate"></param>
        /// <returns></returns>
        public IQueryable<AssetInventoryDetailDto> GetInventoryDetails(Expression<Func<AssetInventoryDetail, bool>> predicate = null)
        {
            return _mapper.ProjectTo<AssetInventoryDetailDto>(_assetInventoryDetailRepository.GetAll(predicate));
        }
        /// <summary>
        /// 检索未经盘点的资产
        /// </summary>
        /// <param name="predicate"></param>
        /// <returns></returns>
        public IQueryable<AssetDto> GetAssetsWithOutInventory(Expression<Func<Asset, bool>> predicate = null)
        {
            return _mapper.ProjectTo<AssetDto>(_assetRepository.GetAll(predicate));
        }
    }
}