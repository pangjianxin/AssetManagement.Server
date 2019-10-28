using AutoMapper;
using Boc.Assets.Application.Dto;
using Boc.Assets.Application.ServiceInterfaces;
using Boc.Assets.Application.ViewModels.AssetCategory;
using Boc.Assets.Domain.Commands.AssetCategory;
using Boc.Assets.Domain.Core.Bus;
using Boc.Assets.Domain.Models.Assets;
using Boc.Assets.Domain.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace Boc.Assets.Application.ServiceImplements
{
    public class AssetCategoryService : IAssetCategoryService
    {
        private readonly IAssetCategoryRepository _assetCategoryRepository;
        private readonly IMapper _mapper;
        private readonly IBus _bus;

        public AssetCategoryService(IAssetCategoryRepository assetCategoryRepository,
            IMapper mapper,
            IBus bus)
        {
            _assetCategoryRepository = assetCategoryRepository;
            _mapper = mapper;
            _bus = bus;
        }

        public async Task ChangeMeteringUnit(ChangeMeteringUnit model)
        {
            var command = _mapper.Map<ChangeMeteringUnitCommand>(model);
            await _bus.SendCommandAsync(command);
        }

        public IQueryable<AssetCategoryDto> Get(Expression<Func<AssetCategory, bool>> predicate = null)
        {
            return _mapper.ProjectTo<AssetCategoryDto>(_assetCategoryRepository.GetAll(predicate));
        }
    }
}