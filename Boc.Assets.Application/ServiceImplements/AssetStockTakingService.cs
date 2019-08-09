using AutoMapper;
using AutoMapper.QueryableExtensions;
using Boc.Assets.Application.Dto;
using Boc.Assets.Application.Pagination;
using Boc.Assets.Application.ServiceInterfaces;
using Boc.Assets.Application.Sieve.Models;
using Boc.Assets.Application.Sieve.Services;
using Boc.Assets.Application.ViewModels.AssetStockTakings;
using Boc.Assets.Domain.Commands.AssetStockTaking;
using Boc.Assets.Domain.Core.Bus;
using Boc.Assets.Domain.Models.Assets;
using Boc.Assets.Domain.Models.AssetStockTakings;
using Boc.Assets.Domain.Repositories;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace Boc.Assets.Application.ServiceImplements
{
    public class AssetStockTakingService : IAssetStockTakingService
    {
        private readonly IMapper _mapper;
        private readonly IBus _bus;
        private readonly ISieveProcessor _sieveProcessor;
        private readonly SieveOptions _sieveOptions;
        private readonly IAssetStockTakingRepository _assetStockTakingRepository;
        private readonly IAssetStockTakingOrganizationRepository _stockTakingOrgRepository;
        private readonly IAssetRepository _assetRepository;
        private readonly IAssetStockTakingDetailRepository _assetStockTakingDetailRepository;

        public AssetStockTakingService(
            IMapper mapper,
            IBus bus,
            ISieveProcessor sieveProcessor,
            IOptions<SieveOptions> options,
            IAssetStockTakingRepository assetStockTakingRepository,
            IAssetStockTakingOrganizationRepository stockTakingOrgRepository,
            IAssetRepository assetRepository,
            IAssetStockTakingDetailRepository assetStockTakingDetailRepository)
        {
            _mapper = mapper;
            _bus = bus;
            _sieveProcessor = sieveProcessor;
            _sieveOptions = options.Value;
            _assetStockTakingRepository = assetStockTakingRepository;
            _stockTakingOrgRepository = stockTakingOrgRepository;
            _assetRepository = assetRepository;
            _assetStockTakingDetailRepository = assetStockTakingDetailRepository;
        }
        public async Task CreateAssetStockTaking(CreateAssetStockTaking model)
        {
            var createAssetStockTakingCommand = _mapper.Map<CreateAssetStockTakingCommand>(model);
            await _bus.SendCommandAsync(createAssetStockTakingCommand);
        }
        /// <summary>
        /// 当前二级行发布的资产盘点任务分页数据
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        public async Task<List<AssetStockTakingDto>> AssetStockTakingList(int year, Expression<Func<AssetStockTaking, bool>> predicate)
        {
            //选择当前二级行下的目标年份的所有记录

            var listResult = _assetStockTakingRepository.GetAll(predicate).ProjectTo<AssetStockTakingDto>(_mapper.ConfigurationProvider);
            return await listResult.ToListAsync();
        }
        /// <summary>
        /// 查询某个资产盘点任务下面的所有参与机构
        /// </summary>
        /// <param name="model"></param>
        /// <param name="stockTakingId"></param>
        /// <returns></returns>
        public async Task<PaginatedList<AssetStockTakingOrgDto>> AssetStockTakingOrgPagination(SieveModel model, Guid stockTakingId)
        {
            var organizations = _stockTakingOrgRepository.GetAll(it => it.AssetStockTakingId == stockTakingId);
            var count = await _sieveProcessor.Apply(model, organizations, applyPagination: false).CountAsync();
            var mappingResult = _sieveProcessor.Apply(model, organizations);
            var pagination = await mappingResult.ToListAsync();
            var mapperResult = _mapper.Map<IEnumerable<AssetStockTakingOrgDto>>(pagination);
            return new PaginatedList<AssetStockTakingOrgDto>(
                _sieveOptions, model.Page, model.PageSize, count, mapperResult);
        }
        /// <summary>
        /// 查询某年的资产盘点参与机构实体列表
        /// </summary>
        /// <returns></returns>
        public async Task<List<AssetStockTakingOrgDto>> StockTakingOrgsInYear(int year, Expression<Func<AssetStockTakingOrganization, bool>> predicate)
        {
            var assetStockTakingOrgs = _stockTakingOrgRepository.GetAll(predicate);
            var mappingResult = await assetStockTakingOrgs.ToListAsync();
            return _mapper.Map<List<AssetStockTakingOrgDto>>(mappingResult);
        }
        /// <summary>
        /// 获取相应年份下面有没有任务记录
        /// </summary>
        /// <param name="year"></param>
        /// <returns></returns>
        public async Task<bool> AnyStockTakingAsync(int year, Expression<Func<AssetStockTaking, bool>> predicate)
        {

            var result = _assetStockTakingRepository.GetAll(predicate);
            return await result.AnyAsync();
        }

        public async Task<bool> AnyStockTakingOrgsAsync(int year, Expression<Func<AssetStockTakingOrganization, bool>> predicate)
        {

            var result = _stockTakingOrgRepository.GetAll(predicate);
            return await result.AnyAsync();
        }

        public async Task<PaginatedList<AssetDto>> AssetsWithOutStockTaking(SieveModel model, Guid stockTakingOrgId, Expression<Func<Asset, bool>> predicate)
        {
            var assetsInStockTakingDetail =
                from detail in _assetStockTakingDetailRepository.GetAll(it => it.AssetStockTakingOrganizationId == stockTakingOrgId)
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

        public async Task CreateAssetStockTakingDetail(CreateAssetStockTakingDetail model)
        {
            var command = _mapper.Map<CreateAssetStockTakingDetailCommand>(model);
            await _bus.SendCommandAsync(command);
        }

        public async Task<PaginatedList<AssetStockTakingDetailDto>> StockTakingDetailPagination(SieveModel model, Guid assetStockTakingOrgId)
        {
            var detailsQueryable =
                from n in _assetStockTakingDetailRepository.GetAll(it =>
                    it.AssetStockTakingOrganizationId == assetStockTakingOrgId)
                select n;
            var mapperResult = detailsQueryable.ProjectTo<AssetStockTakingDetailDto>(_mapper.ConfigurationProvider);
            var count = await _sieveProcessor.Apply(model, mapperResult, applyPagination: false).CountAsync();
            var paginatedList = _sieveProcessor.Apply(model, mapperResult);
            return new PaginatedList<AssetStockTakingDetailDto>(_sieveOptions, model.Page, model.PageSize, count, paginatedList);
        }
    }
}