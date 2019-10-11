using AutoMapper;
using AutoMapper.QueryableExtensions;
using Boc.Assets.Application.Dto;
using Boc.Assets.Application.Pagination;
using Boc.Assets.Application.ServiceInterfaces;
using Boc.Assets.Application.Sieve.Models;
using Boc.Assets.Application.Sieve.Services;
using Boc.Assets.Application.ViewModels.Assets;
using Boc.Assets.Domain.Commands.Assets;
using Boc.Assets.Domain.Core.Bus;
using Boc.Assets.Domain.Models.Applies;
using Boc.Assets.Domain.Repositories;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
using System;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace Boc.Assets.Application.ServiceImplements
{
    public class AssetApplyService : IAssetApplyService
    {
        private readonly IAssetApplyRepository _assetApplyRepository;
        private readonly IMapper _mapper;
        private readonly IBus _bus;
        private readonly ISieveProcessor _sieveProcessor;
        private readonly SieveOptions _sieveOptions;

        public AssetApplyService(IAssetApplyRepository assetApplyRepository,
            IMapper mapper,
            IBus bus,
            ISieveProcessor sieveProcessor,
            IOptions<SieveOptions> options)
        {
            _assetApplyRepository = assetApplyRepository;
            _mapper = mapper;
            _bus = bus;
            _sieveProcessor = sieveProcessor;
            _sieveOptions = options.Value;
        }
        /// <summary>
        /// 获取分页数据
        /// </summary>
        /// <param name="model"></param>
        /// <param name="predicate"></param>
        /// <returns></returns>
        public async Task<PaginatedList<AssetApplyDto>> PaginationAsync(SieveModel model, Expression<Func<AssetApply, bool>> predicate = null)
        {
            var entities = _assetApplyRepository.GetAll(predicate);
            var count = await _sieveProcessor.Apply(model, entities, applyPagination: false).CountAsync();
            var result = _sieveProcessor.Apply(model, entities)
                    .ProjectTo<AssetApplyDto>(_mapper.ConfigurationProvider);
            var pagination = await result.ToListAsync();
            return new PaginatedList<AssetApplyDto>(
                _sieveOptions, model.Page, model.PageSize, count, pagination);
        }
        /// <summary>
        /// 删除资产申请
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        public async Task RemoveAsync(RemoveAssetApply model)
        {
            var command = _mapper.Map<RemoveAssetApplyCommand>(model);
            await _bus.SendCommandAsync(command);
        }
        /// <summary>
        /// 发起资产申请
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        public async Task ApplyAssetAsync(ApplyAsset model)
        {
            var command = _mapper.Map<CreateAssetApplyCommand>(model);
            await _bus.SendCommandAsync(command);
        }
        /// <summary>
        /// 撤销资产申请
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        public async Task RevokeAsync(RevokeAssetApply model)
        {
            var command = _mapper.Map<RevokeAssetApplyCommand>(model);
            await _bus.SendCommandAsync(command);
        }
        /// <summary>
        /// 处理资产申请
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        public async Task HandleAsync(HandleAssetApply model)
        {
            var command = _mapper.Map<HandleAssetApplyCommand>(model);
            await _bus.SendCommandAsync(command);
        }
    }
}