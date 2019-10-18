using Boc.Assets.Application.Dto;
using Boc.Assets.Application.ViewModels.AssetInventories;
using Boc.Assets.Domain.Models.AssetInventories;
using Boc.Assets.Domain.Models.Assets;
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
        //检查资产盘点参与机构
        IQueryable<AssetInventoryResgiterDto> GetInventoryRegisters(Expression<Func<AssetInventoryRegister, bool>> predicate = null);
        //检查资产盘点清单
        IQueryable<AssetInventoryDetailDto> GetInventoryDetails(Expression<Func<AssetInventoryDetail, bool>> predicate = null);
        //检查未经盘点的资产
        IQueryable<AssetDto> GetAssetsWithOutInventory(Expression<Func<Asset, bool>> predicate = null);
        //创建资产明细
        Task CreatAssetInventoryDetailAsync(CreateAssetInventoryDetail model);

    }
}