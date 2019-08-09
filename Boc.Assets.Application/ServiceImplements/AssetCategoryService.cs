using AutoMapper;
using AutoMapper.QueryableExtensions;
using Boc.Assets.Application.Dto;
using Boc.Assets.Application.ServiceInterfaces;
using Boc.Assets.Application.Sieve.Models;
using Boc.Assets.Application.Sieve.Services;
using Boc.Assets.Application.ViewModels.AssetCategory;
using Boc.Assets.Domain.Commands.AssetCategory;
using Boc.Assets.Domain.Core.Bus;
using Boc.Assets.Domain.Core.SharedKernel;
using Boc.Assets.Domain.Models.Assets;
using Boc.Assets.Domain.Repositories;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace Boc.Assets.Application.Pagination
{
    public class AssetCategoryService : IAssetCategoryService
    {
        private readonly IAssetCategoryRepository _assetCategoryRepository;
        private readonly IMapper _mapper;
        private readonly IBus _bus;
        private readonly ISieveProcessor _sieveProcessor;
        private readonly IUser _user;
        private readonly SieveOptions _sieveOptions;

        public AssetCategoryService(IAssetCategoryRepository assetCategoryRepository,
            IMapper mapper,
            IBus bus,
            IUser user,
            ISieveProcessor sieveProcessor,
            IOptions<SieveOptions> sieveOptions)
        {
            _assetCategoryRepository = assetCategoryRepository;
            _mapper = mapper;
            _bus = bus;
            _sieveProcessor = sieveProcessor;
            _user = user;
            _sieveOptions = sieveOptions.Value;
        }

        public async Task ChangeMeteringUnit(ChangeMeteringUnit model)
        {
            var command = _mapper.Map<ChangeMeteringUnitCommand>(model);
            await _bus.SendCommandAsync(command);
        }

        public IEnumerable<dynamic> GetMeteringUnits()
        {
            List<dynamic> dic = new List<dynamic>();
            foreach (var item in Enum.GetValues(typeof(AssetMeteringUnit)))
            {
                dic.Add(new { name = item.ToString(), value = (int)item });
            }
            return dic;
        }

        public async Task<PaginatedList<AssetCategoryDto>> PaginationAsync(SieveModel model, Expression<Func<AssetCategory, bool>> predicate = null)
        {
            var entities = _assetCategoryRepository.GetAll(predicate);
            var count = await _sieveProcessor.Apply(model, entities, applyPagination: false).CountAsync();
            var result = _sieveProcessor.Apply(model, entities)
                .ProjectTo<AssetCategoryDto>(_mapper.ConfigurationProvider);
            var pagination = await result.ToListAsync();
            return new PaginatedList<AssetCategoryDto>(
                _sieveOptions, model.Page, model.PageSize, count, pagination);
        }
    }
}