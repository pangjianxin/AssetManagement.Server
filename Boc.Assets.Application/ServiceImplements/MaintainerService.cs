using AutoMapper;
using AutoMapper.QueryableExtensions;
using Boc.Assets.Application.Dto;
using Boc.Assets.Application.Pagination;
using Boc.Assets.Application.ServiceInterfaces;
using Boc.Assets.Application.Sieve.Models;
using Boc.Assets.Application.Sieve.Services;
using Boc.Assets.Application.ViewModels.Maintainers;
using Boc.Assets.Domain.Commands.Maintainers;
using Boc.Assets.Domain.Core.Bus;
using Boc.Assets.Domain.Models.Assets;
using Boc.Assets.Domain.Repositories;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace Boc.Assets.Application.ServiceImplements
{
    public class MaintainerService : IMaintainerService
    {
        private readonly IBus _bus;
        private readonly IMapper _mapper;
        private readonly ISieveProcessor _sievingProcessor;
        private readonly SieveOptions _sieveOptions;
        private readonly IMaintainerRepository _maintainerRepository;

        public MaintainerService(
            IBus bus,
            IMapper mapper,
            ISieveProcessor sievingProcessor,
            IOptions<SieveOptions> sieveOptions,
            IMaintainerRepository maintainerRepository)
        {
            _bus = bus;
            _mapper = mapper;
            _sievingProcessor = sievingProcessor;
            _sieveOptions = sieveOptions.Value;
            _maintainerRepository = maintainerRepository;
        }
        public async Task AddMaintainerAsync(AddMaintainer model)
        {
            var command = _mapper.Map<AddMaintainerCommand>(model);
            await _bus.SendCommandAsync(command);
        }

        public async Task<bool> AnyMaintainerAsync(Guid assetCategoryId, string org2)
        {
            return await _maintainerRepository.AnyAsync(it => it.AssetCategoryId == assetCategoryId && it.Org2 == org2);
        }
        public async Task DeleteAsync(DeleteMaintainer model)
        {
            var command = _mapper.Map<DeleteMaintainerCommand>(model);
            await _bus.SendCommandAsync(command);
        }
        /// <summary>
        /// 根据资产分类查找相对应的维修商
        /// </summary>
        /// <param name="categoryId"></param>
        /// <param name="org2"></param>
        /// <returns></returns>
        public Task<List<MaintainerDto>> MaintainersByCategoryId(Guid categoryId, string org2)
        {
            var entities = _maintainerRepository.GetAll(it => it.AssetCategoryId == categoryId && it.Org2 == org2);
            return entities.ProjectTo<MaintainerDto>(_mapper.ConfigurationProvider).ToListAsync();
        }

        public async Task<PaginatedList<MaintainerDto>> PaginationAsync(SieveModel model, Expression<Func<Maintainer, bool>> predicate = null)
        {
            var entities = _maintainerRepository.GetAll(predicate);
            var count = await _sievingProcessor.Apply(model, entities, applyPagination: false).CountAsync();
            var result = _sievingProcessor.Apply(model, entities)
                .ProjectTo<MaintainerDto>(_mapper.ConfigurationProvider);
            var pagination = await result.ToListAsync();
            return new PaginatedList<MaintainerDto>(
                _sieveOptions, model.Page, model.PageSize, count, pagination);
        }
    }
}