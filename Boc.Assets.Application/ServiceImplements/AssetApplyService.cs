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
    public class AssetApplyService : IAssetApplyService
    {
        private readonly IAssetApplyRepository _assetApplyRepository;
        private readonly IMapper _mapper;
        private readonly IBus _bus;

        public AssetApplyService(IAssetApplyRepository assetApplyRepository,
            IMapper mapper,
            IBus bus)
        {
            _assetApplyRepository = assetApplyRepository;
            _mapper = mapper;
            _bus = bus;
        }
        /// <summary>
        /// OData数据接口
        /// </summary>
        /// <param name="predicate"></param>
        /// <returns></returns>
        public IQueryable<AssetApplyDto> Get(Expression<Func<AssetApply, bool>> predicate = null)
        {
            return _mapper.ProjectTo<AssetApplyDto>(_assetApplyRepository.GetAll(predicate));
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