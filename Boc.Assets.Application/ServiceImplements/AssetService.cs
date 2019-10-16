using AutoMapper;
using AutoMapper.QueryableExtensions;
using Boc.Assets.Application.Dto;
using Boc.Assets.Application.Pagination;
using Boc.Assets.Application.ServiceInterfaces;
using Boc.Assets.Application.ViewModels.Assets;
using Boc.Assets.Domain.Commands.Assets;
using Boc.Assets.Domain.Core.Bus;
using Boc.Assets.Domain.Models.Assets;
using Boc.Assets.Domain.Repositories;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
using Sieve.Models;
using Sieve.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace Boc.Assets.Application.ServiceImplements
{
    public class AssetService : IAssetService
    {
        private readonly IMapper _mapper;
        private readonly IAssetRepository _assetRepository;
        private readonly IAssetInventoryDetailRepository _assetInventoryDetailRepository;
        private readonly IBus _bus;
        private readonly ISieveProcessor _sieveProcessor;
        private readonly SieveOptions _sieveOptions;

        public AssetService(IMapper mapper,
            IAssetRepository assetRepository,
            IAssetInventoryDetailRepository assetStockTakingDetailRepository,
            IBus bus,
            ISieveProcessor sieveProcessor,
            IOptions<SieveOptions> options)
        {
            _mapper = mapper;
            _assetRepository = assetRepository;
            _assetInventoryDetailRepository = assetStockTakingDetailRepository;
            _bus = bus;
            _sieveProcessor = sieveProcessor;
            _sieveOptions = options.Value;
        }
        #region update

        public async Task<IEnumerable<dynamic>> CategoriesByManagerOrg(Expression<Func<Asset, bool>> predicate)
        {
            var result = from asset in _assetRepository.GetAll(predicate)
                         group asset by asset.OrganizationInCharge.OrgIdentifier
                into final
                         select new { name = final.Key, value = final.Count() };
            return await result.ToListAsync();
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

        #endregion
        #region read

        public async Task<IEnumerable<dynamic>> CategoriesByThirdLevelAsync(Expression<Func<Asset, bool>> predicate)
        {
            var result = from n in _assetRepository.GetAll(predicate)
                         group n by new { n.AssetCategory.AssetThirdLevelCategory, n.AssetCategory.Id }
                into m

                         select new { name = m.Key.AssetThirdLevelCategory, value = m.Count(), description = m.Key.Id };
            return await result.ToListAsync();
        }

        public async Task<IEnumerable<dynamic>> CategoriesByStatusAsync(Expression<Func<Asset, bool>> predicate)
        {
            var result = from n in _assetRepository.GetAll(predicate)
                         group n by n.AssetStatus
                into m
                         select new { name = m.Key.ToString(), value = m.Count() };
            return await result.ToListAsync();
        }
        public async Task<PaginatedList<AssetDto>> PaginationAsync(SieveModel model, Expression<Func<Asset, bool>> predicate)
        {
            var entities = _assetRepository.GetAll(predicate);
            var count = await _sieveProcessor.Apply(model, entities, applyPagination: false).CountAsync();
            var result = _sieveProcessor.Apply(model, entities).ProjectTo<AssetDto>(_mapper.ConfigurationProvider);
            var pagination = await result.ToListAsync();
            return new PaginatedList<AssetDto>(_sieveOptions, model.Page, model.PageSize, count, pagination);
        }
        #endregion
    }
}