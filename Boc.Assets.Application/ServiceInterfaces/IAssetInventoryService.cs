using Boc.Assets.Application.Dto;
using Boc.Assets.Application.ViewModels.AssetInventories;
using Boc.Assets.Domain.Models.AssetInventories;
using System;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace Boc.Assets.Application.ServiceInterfaces
{
    public interface IAssetInventoryService : IApplicationService
    {
        //创建资产盘点
        Task CreateAssetInventoryAsync(CreateAssetInventory model);
        //检查已存在的资产盘点
        IQueryable<AssetInventoryDto> GetInventories(Expression<Func<AssetInventory, bool>> predicate = null);
    }
}