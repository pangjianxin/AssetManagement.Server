using AutoMapper;
using Boc.Assets.Application.Dto;
using Boc.Assets.Application.ServiceInterfaces;
using Boc.Assets.Application.ViewModels.AssetInventories;
using Boc.Assets.Domain.Commands.AssetInventory;
using Boc.Assets.Domain.Core.Bus;
using Boc.Assets.Domain.Models.AssetInventories;
using Boc.Assets.Domain.Repositories;
using System;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace Boc.Assets.Application.ServiceImplements
{
    public class AssetInventoryDetailService : IAssetInventoryDetailService
    {
        private readonly IAssetInventoryDetailRepository _detailRepository;
        private readonly IMapper _mapper;
        private readonly IBus _bus;

        public AssetInventoryDetailService(IAssetInventoryDetailRepository detailRepository,
            IMapper mapper,
            IBus bus)
        {
            _detailRepository = detailRepository;
            _mapper = mapper;
            _bus = bus;
        }
        public IQueryable<AssetInventoryDetailDto> Get(Expression<Func<AssetInventoryDetail, bool>> predicate = null)
        {
            return _mapper.ProjectTo<AssetInventoryDetailDto>(_detailRepository.GetAll(predicate));
        }
        /// <summary>
        /// 创建资产盘点明细
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        public async Task CreateAsync(CreateAssetInventoryDetail model)
        {
            var command = _mapper.Map<CreateAssetInventoryDetailCommand>(model);
            await _bus.SendCommandAsync(command);
        }
    }
}