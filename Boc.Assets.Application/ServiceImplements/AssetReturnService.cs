using AutoMapper;
using Boc.Assets.Application.Dto;
using Boc.Assets.Application.ServiceInterfaces;
using Boc.Assets.Application.ViewModels.Assets;
using Boc.Assets.Domain.Commands.Assets;
using Boc.Assets.Domain.Core.Bus;
using Boc.Assets.Domain.Models.Applies;
using Boc.Assets.Domain.Repositories;
using System;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace Boc.Assets.Application.ServiceImplements
{
    public class AssetReturnService : IAssetReturnService
    {
        private readonly IAssetReturnRepository _assetReturnRepository;
        private readonly IMapper _mapper;
        private readonly IBus _bus;
        public AssetReturnService(
            IAssetReturnRepository assetReturnRepository,
            IMapper mapper,
            IBus bus)
        {
            _assetReturnRepository = assetReturnRepository;
            _mapper = mapper;
            _bus = bus;
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

        public IQueryable<AssetReturnDto> Get(Expression<Func<AssetReturn, bool>> predicate = null)
        {
            return _mapper.ProjectTo<AssetReturnDto>(_assetReturnRepository.GetAll(predicate));
        }
    }
}