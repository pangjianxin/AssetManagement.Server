using AutoMapper;
using Boc.Assets.Application.Dto;
using Boc.Assets.Application.ServiceInterfaces;
using Boc.Assets.Application.ViewModels.Assets;
using Boc.Assets.Domain.Commands.Assets;
using Boc.Assets.Domain.Core.Bus;
using Boc.Assets.Domain.Models.Assets;
using Boc.Assets.Domain.Repositories;
using Microsoft.EntityFrameworkCore;
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

        public AssetService(IMapper mapper,
            IAssetRepository assetRepository,
            IAssetInventoryDetailRepository assetStockTakingDetailRepository,
            IBus bus)
        {
            _mapper = mapper;
            _assetRepository = assetRepository;
            _assetInventoryDetailRepository = assetStockTakingDetailRepository;
            _bus = bus;
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

        public IQueryable<AssetDto> Get(Expression<Func<Asset, bool>> predicate = null)
        {
            return _mapper.ProjectTo<AssetDto>(_assetRepository.GetAll(predicate));
        }

        #endregion
    }
}