using AutoMapper;
using AutoMapper.QueryableExtensions;
using Boc.Assets.Application.Dto;
using Boc.Assets.Application.Pagination;
using Boc.Assets.Application.ServiceInterfaces;
using Boc.Assets.Application.ViewModels.AssetInventories;
using Boc.Assets.Domain.Commands.AssetInventory;
using Boc.Assets.Domain.Core.Bus;
using Boc.Assets.Domain.Models.AssetInventories;
using Boc.Assets.Domain.Models.Assets;
using Boc.Assets.Domain.Repositories;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;
using Sieve.Models;
using Sieve.Services;

namespace Boc.Assets.Application.ServiceImplements
{
    public class AssetInventoryService : IAssetInventoryService
    {
        private readonly IMapper _mapper;
        private readonly IBus _bus;
        private readonly ISieveProcessor _sieveProcessor;
        private readonly SieveOptions _sieveOptions;
        private readonly IAssetInventoryRepository _assetInventoryRepository;
        private readonly IAssetInventoryRegisterRepository _inventoryRegisterRepository;
        private readonly IAssetRepository _assetRepository;
        private readonly IAssetInventoryDetailRepository _assetInventoryDetailRepository;

        public AssetInventoryService(
            IMapper mapper,
            IBus bus,
            ISieveProcessor sieveProcessor,
            IOptions<SieveOptions> options,
            IAssetInventoryRepository assetInventoryRepository,
            IAssetInventoryRegisterRepository inventoryRegisterRepository,
            IAssetRepository assetRepository,
            IAssetInventoryDetailRepository assetInventoryDetailRepository)
        {
            _mapper = mapper;
            _bus = bus;
            _sieveProcessor = sieveProcessor;
            _sieveOptions = options.Value;
            _assetInventoryRepository = assetInventoryRepository;
            _inventoryRegisterRepository = inventoryRegisterRepository;
            _assetRepository = assetRepository;
            _assetInventoryDetailRepository = assetInventoryDetailRepository;
        }
        public async Task CreateAssetInventoryAsync(CreateAssetInventory model)
        {
            var createAssetStockTakingCommand = _mapper.Map<CreateAssetInventoryCommand>(model);
            await _bus.SendCommandAsync(createAssetStockTakingCommand);
        }

        /// <summary>
        /// 当前二级行发布的资产盘点任务分页数据
        /// </summary>
        /// <param name="year"></param>
        /// <param name="predicate"></param>
        /// <returns></returns>
        public async Task<List<AssetInventoryDto>> AssetInventoriesAsync(int year, Expression<Func<AssetInventory, bool>> predicate)
        {
            //选择当前二级行下的目标年份的所有记录

            var listResult = _assetInventoryRepository.GetAll(predicate).ProjectTo<AssetInventoryDto>(_mapper.ConfigurationProvider);
            return await listResult.ToListAsync();
        }
        /// <summary>
        /// 查询某个资产盘点任务下面的所有参与机构
        /// </summary>
        /// <param name="model"></param>
        /// <param name="stockTakingId"></param>
        /// <returns></returns>
        public async Task<PaginatedList<AssetInventoryResgiterDto>> AssetInventoriesPaginationAsync(SieveModel model, Guid stockTakingId)
        {
            var organizations = _inventoryRegisterRepository.GetAll(it => it.AssetInventoryId == stockTakingId);
            var count = await _sieveProcessor.Apply(model, organizations, applyPagination: false).CountAsync();
            var mappingResult = _sieveProcessor.Apply(model, organizations);
            var pagination = await mappingResult.ToListAsync();
            var mapperResult = _mapper.Map<IEnumerable<AssetInventoryResgiterDto>>(pagination);
            return new PaginatedList<AssetInventoryResgiterDto>(
                _sieveOptions, model.Page, model.PageSize, count, mapperResult);
        }
        /// <summary>
        /// 查询某年的资产盘点参与机构实体列表
        /// </summary>
        /// <returns></returns>
        public async Task<List<AssetInventoryResgiterDto>> AssetInventoriesByYearAsync(int year, Expression<Func<AssetInventoryRegister, bool>> predicate)
        {
            var assetStockTakingOrgs = _inventoryRegisterRepository.GetAll(predicate);
            var mappingResult = await assetStockTakingOrgs.ToListAsync();
            return _mapper.Map<List<AssetInventoryResgiterDto>>(mappingResult);
        }

        /// <summary>
        /// 获取相应年份下面有没有任务记录
        /// </summary>
        /// <param name="year"></param>
        /// <param name="predicate"></param>
        /// <returns></returns>
        public async Task<bool> AnyAssetInventoryAsync(int year, Expression<Func<AssetInventory, bool>> predicate)
        {

            var result = _assetInventoryRepository.GetAll(predicate);
            return await result.AnyAsync();
        }

        public async Task<bool> AnyAssetInventoryRegistersAsync(int year, Expression<Func<AssetInventoryRegister, bool>> predicate)
        {

            var result = _inventoryRegisterRepository.GetAll(predicate);
            return await result.AnyAsync();
        }

        public async Task<PaginatedList<AssetDto>> AssetsWithOutInventoriesAsync(SieveModel model, Guid stockTakingOrgId, Expression<Func<Asset, bool>> predicate)
        {
            var assetsInStockTakingDetail =
                from detail in _assetInventoryDetailRepository.GetAll(it => it.AssetInventoryRegisterId == stockTakingOrgId)
                select detail.Asset.Id;
            var assetWithOutStockTaking =
                from asset in _assetRepository.GetAll(predicate)
                where !assetsInStockTakingDetail.Contains(asset.Id)
                select asset;
            var mapperResult = _mapper.ProjectTo<AssetDto>(assetWithOutStockTaking);
            var count = await _sieveProcessor.Apply(model, mapperResult, applyPagination: false).CountAsync();
            var paginatedList = _sieveProcessor.Apply(model, mapperResult);
            return new PaginatedList<AssetDto>(_sieveOptions, model.Page, model.PageSize, count, paginatedList);
        }

        public async Task CreatAssetInventoryDetailAsync(CreateAssetInventoryDetail model)
        {
            var command = _mapper.Map<CreateAssetInventoryDetailCommand>(model);
            await _bus.SendCommandAsync(command);
        }

        public async Task<PaginatedList<AssetInventoryDetailDto>> AssetInventoryDetailPaginationAsync(SieveModel model, Guid assetStockTakingOrgId)
        {
            var detailsQueryable =
                from n in _assetInventoryDetailRepository.GetAll(it =>
                    it.AssetInventoryRegisterId == assetStockTakingOrgId)
                select n;
            var mapperResult = detailsQueryable.ProjectTo<AssetInventoryDetailDto>(_mapper.ConfigurationProvider);
            var count = await _sieveProcessor.Apply(model, mapperResult, applyPagination: false).CountAsync();
            var paginatedList = _sieveProcessor.Apply(model, mapperResult);
            return new PaginatedList<AssetInventoryDetailDto>(_sieveOptions, model.Page, model.PageSize, count, paginatedList);
        }
    }
}