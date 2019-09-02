using Boc.Assets.Domain.Core.SharedKernel;
using Boc.Assets.Domain.Models.Assets;
using Boc.Assets.Domain.Models.Assets.Audit;
using Boc.Assets.Domain.Models.Organizations;
using System;
using System.Threading.Tasks;

namespace Boc.Assets.Domain.Services
{
    public interface IAssetDomainService
    {
        /// <summary>
        /// 创建一个资产申请
        /// </summary>
        /// <param name="user"></param>
        /// <param name="targetOrg"></param>
        /// <param name="assetCategoryId"></param>
        /// <param name="thirdLevelCategory"></param>
        /// <param name="message"></param>
        /// <returns></returns>
        Task<AssetApply> CreateAssetApply(IUser user, Organization targetOrg, Guid assetCategoryId, string thirdLevelCategory, string message);
        /// <summary>
        /// 创建资产交回
        /// </summary>
        /// <param name="asset"></param>
        /// <param name="user"></param>
        /// <param name="targetOrg"></param>
        /// <param name="message"></param>
        /// <returns></returns>
        Task<AssetReturn> CreateAssetReturn(Asset asset, IUser user, Organization targetOrg, string message);
        /// <summary>
        /// 创建一个资产调换
        /// </summary>
        /// <param name="asset"></param>
        /// <param name="user"></param>
        /// <param name="targetOrg"></param>
        /// <param name="exchangeOrg"></param>
        /// <param name="message"></param>
        /// <returns></returns>
        Task<AssetExchange> CreateAssetExchange(Asset asset, IUser user, Organization targetOrg, Organization exchangeOrg, string message);
        /// <summary>
        /// 处理资产交回
        /// </summary>
        /// <param name="asset"></param>
        /// <param name="apply"></param>
        /// <param name="handleMessage"></param>
        /// <returns></returns>
        Task HandleAssetReturn(Asset asset, AssetReturn apply, string handleMessage);
        /// <summary>
        /// 处理资产申请
        /// </summary>
        /// <param name="asset"></param>
        /// <param name="apply"></param>
        /// <param name="handleMessage"></param>
        /// <returns></returns>
        Task HandleAssetApply(Asset asset, AssetApply apply, string handleMessage);
        /// <summary>
        /// 处理资产调换
        /// </summary>
        /// <param name="asset"></param>
        /// <param name="apply"></param>
        /// <param name="handleMessage"></param>
        /// <returns></returns>
        Task HandleAssetExchange(Asset asset, AssetExchange apply, string handleMessage);
        /// <summary>
        /// 撤销资产申请
        /// </summary>
        /// <param name="apply"></param>
        /// <param name="handleMessage"></param>
        void RevokeAssetApply(AssetApply apply, string handleMessage);
        /// <summary>
        /// 撤销资产交回
        /// </summary>
        /// <param name="asset"></param>
        /// <param name="apply"></param>
        /// <param name="handleMessage"></param>
        void RevokeAssetReturn(Asset asset, AssetReturn apply, string handleMessage);
        /// <summary>
        /// 撤销资产调换
        /// </summary>
        /// <param name="asset"></param>
        /// <param name="apply"></param>
        /// <param name="handleMessage"></param>
        void RevokeAssetExchange(Asset asset, AssetExchange apply, string handleMessage);
        /// <summary>
        /// 删除资产申请
        /// </summary>
        /// <param name="apply"></param>
        /// <param name="handleMessage"></param>
        void RemoveAssetApply(AssetApply apply, string handleMessage);

        /// <summary>
        /// 删除资产交回
        /// </summary>
        /// <param name="asset"></param>
        /// <param name="apply"></param>
        /// <param name="handleMessage"></param>
        void RemoveAssetReturn(Asset asset, AssetReturn apply, string handleMessage);

        /// <summary>
        /// 删除资产调换
        /// </summary>
        /// <param name="asset"></param>
        /// <param name="apply"></param>
        /// <param name="handleMessage"></param>
        void RemoveAssetExchange(Asset asset, AssetExchange apply, string handleMessage);
    }
}