using Boc.Assets.Application.Dto;
using Boc.Assets.Application.ViewModels.AssetInventories;
using Boc.Assets.Domain.Models.AssetInventories;
using System;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace Boc.Assets.Application.ServiceInterfaces
{
    public interface IAssetInventoryDetailService : IApplicationService
    {
        IQueryable<AssetInventoryDetailDto> Get(Expression<Func<AssetInventoryDetail, bool>> predicate = null);
        //创建资产明细
        Task CreateAsync(CreateAssetInventoryDetail model);
    }
}