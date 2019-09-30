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
using Boc.Assets.Domain.Core.SharedKernel;
using Boc.Assets.Domain.Repositories;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
using System;
using System.Linq.Expressions;
using System.Threading.Tasks;
using Boc.Assets.Domain.Models.Applies;

namespace Boc.Assets.Application.ServiceImplements
{
    public class AssetReturnService : IAssetReturnService
    {
        private readonly IAssetReturnRepository _assetReturnRepository;
        private readonly IMapper _mapper;
        private readonly IBus _bus;
        private readonly ISieveProcessor _sieveProcessor;
        private readonly IUser _user;
        private readonly SieveOptions _sieveOptions;
        public AssetReturnService(
            IAssetReturnRepository assetReturnRepository,
            IMapper mapper,
            IBus bus,
            ISieveProcessor sieveProcessor,
            IUser user,
            IOptions<SieveOptions> options)
        {
            _assetReturnRepository = assetReturnRepository;
            _mapper = mapper;
            _bus = bus;
            _sieveProcessor = sieveProcessor;
            _user = user;
            _sieveOptions = options.Value;
        }

        public async Task<PaginatedList<AssetReturnDto>> PaginationAsync(SieveModel model, Expression<Func<AssetReturn, bool>> predicate)
        {
            var entities = _assetReturnRepository.GetAll(predicate);
            var count = await _sieveProcessor.Apply(model, entities, applyPagination: false).CountAsync();
            var result = _sieveProcessor.Apply(model, entities)
                .ProjectTo<AssetReturnDto>(_mapper.ConfigurationProvider);
            var pagination = await result.ToListAsync();
            return new PaginatedList<AssetReturnDto>(
                _sieveOptions, model.Page, model.PageSize, count, pagination);
        }
        /// <summary>
        /// 删除资产交回申请
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        public async Task<bool> RemoveAssetReturnAsync(RemoveAssetReturn model)
        {
            var command = _mapper.Map<RemoveAssetReturnCommand>(model);
            return await _bus.SendCommandAsync(command);
        }
        /// <summary>
        /// 撤销资产交回申请
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        public async Task RevokeAssetReturnAsync(RevokeAssetReturn model)
        {
            var command = _mapper.Map<RevokeAssetReturnCommand>(model);
            await _bus.SendCommandAsync(command);
        }
        /// <summary>
        /// 处理资产交回申请
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        public async Task HandleAssetReturnAsync(HandleAssetReturn model)
        {
            var command = _mapper.Map<HandleAssetReturnCommand>(model);
            await _bus.SendCommandAsync(command);
        }
        /// <summary>
        /// 创建资产交回申请
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        public async Task CreateAssetReturnAsync(ReturnAsset model)
        {
            var command = _mapper.Map<CreateAssetReturnCommand>(model);
            await _bus.SendCommandAsync(command);
        }
    }
}