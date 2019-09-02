using Boc.Assets.Domain.Core.SharedKernel;
using Boc.Assets.Domain.Models.Assets;
using Boc.Assets.Domain.Models.Assets.Audit;
using Boc.Assets.Domain.Models.Organizations;
using Boc.Assets.Domain.Repositories;
using Boc.Assets.Domain.Services;
using System;
using System.Threading.Tasks;

namespace Boc.Assets.Infrastructure.Services
{
    public class AssetDomainService : IAssetDomainService
    {
        private readonly IAssetRepository _assetRepository;
        private readonly IAssetReturnRepository _assetReturnRepository;
        private readonly IAssetDeployRepository _assetDeployRepository;
        private readonly IAssetApplyRepository _assetApplyRepository;
        private readonly IAssetExchangeRepository _assetExchangeRepository;


        public AssetDomainService(IAssetRepository assetRepository,
            IAssetReturnRepository assetReturnRepository,
            IAssetDeployRepository assetDeployRepository,
            IAssetApplyRepository assetApplyRepository,
            IAssetExchangeRepository assetExchangeRepository)
        {
            _assetRepository = assetRepository;
            _assetReturnRepository = assetReturnRepository;
            _assetDeployRepository = assetDeployRepository;
            _assetApplyRepository = assetApplyRepository;
            _assetExchangeRepository = assetExchangeRepository;
        }
        /// <inheritdoc />
        /// <summary>
        /// 处理资产交回
        /// </summary>
        /// <param name="asset"></param>
        /// <param name="apply"></param>
        /// <param name="handleMessage"></param>
        /// <returns></returns>
        public async Task HandleAssetReturn(Asset asset, AssetReturn apply, string handleMessage)
        {
            //资产状态变化
            asset.HandleReturn(apply.TargetOrgIdentifier, apply.RequestOrgNam);
            //申请状态变化
            apply.Complete(handleMessage);
            //产出一条调配记录
            var deploy = new AssetDeploy
            {
                Id = Guid.NewGuid(),
                AssetDeployCategory = AssetDeployCategory.资产交回,
                CreateDateTime = DateTime.Now,
                AssetTagNumber = asset.AssetTagNumber,
                AssetName = asset.AssetName,
                ExportOrgInfo = new OrganizationInfo()
                {
                    Org2 = apply.Org2,
                    OrgId = apply.RequestOrgId,
                    OrgIdentifier = apply.RequestOrgIdentifier,
                    OrgNam = apply.RequestOrgNam
                },
                ImportOrgInfo = new OrganizationInfo()
                {
                    OrgId = apply.TargetOrgId,
                    OrgIdentifier = apply.TargetOrgIdentifier,
                    OrgNam = apply.TargetOrgNam
                },
                AuthorizeOrgInfo = new OrganizationInfo()
                {
                    OrgId = apply.TargetOrgId,
                    OrgIdentifier = apply.TargetOrgIdentifier,
                    OrgNam = apply.TargetOrgNam
                }
            };
            //保存到数据库
            _assetRepository.Update(asset);
            _assetReturnRepository.Update(apply);
            await _assetDeployRepository.AddAsync(deploy);
        }
        /// <inheritdoc />
        /// <summary>
        /// 处理资产申请
        /// </summary>
        /// <param name="asset"></param>
        /// <param name="apply"></param>
        /// <param name="handleMessage"></param>
        /// <returns></returns>
        public async Task HandleAssetApply(Asset asset, AssetApply apply, string handleMessage)
        {
            //资产状态变化
            asset.HandleApply(apply.TargetOrgIdentifier, apply.TargetOrgNam);
            //申请状态变化
            apply.Complete(handleMessage);
            //产生一条新纪录
            var deploy = new AssetDeploy
            {
                Id = Guid.NewGuid(),
                AssetDeployCategory = AssetDeployCategory.资产申请,
                CreateDateTime = DateTime.Now,
                AssetTagNumber = asset.AssetTagNumber,
                AssetName = asset.AssetName,
                ExportOrgInfo = new OrganizationInfo
                {
                    Org2 = apply.Org2,
                    OrgId = apply.TargetOrgId,
                    OrgIdentifier = apply.TargetOrgIdentifier,
                    OrgNam = apply.TargetOrgNam
                },
                ImportOrgInfo = new OrganizationInfo
                {
                    Org2 = apply.Org2,
                    OrgId = apply.RequestOrgId,
                    OrgIdentifier = apply.RequestOrgIdentifier,
                    OrgNam = apply.RequestOrgNam
                },
                AuthorizeOrgInfo = new OrganizationInfo
                {
                    Org2 = apply.Org2,
                    OrgId = apply.TargetOrgId,
                    OrgIdentifier = apply.TargetOrgIdentifier,
                    OrgNam = apply.TargetOrgNam
                }
            };
            //保存到数据库
            _assetRepository.Update(asset);
            _assetApplyRepository.Update(apply);
            await _assetDeployRepository.AddAsync(deploy);
        }
        /// <inheritdoc />
        /// <summary>
        /// 处理资产调换
        /// </summary>
        /// <param name="asset"></param>
        /// <param name="apply"></param>
        /// <param name="handleMessage"></param>
        /// <returns></returns>
        public async Task HandleAssetExchange(Asset asset, AssetExchange apply, string handleMessage)
        {
            //资产状态变化
            asset.HandleExchange(apply.ExchangeOrgIdentifier, apply.ExchangeOrgNam);
            //申请状态变化
            apply.Complete(handleMessage);
            //产生一条新的记录
            var deploy = new AssetDeploy()
            {
                Id = Guid.NewGuid(),
                AssetDeployCategory = AssetDeployCategory.资产机构间调配,
                CreateDateTime = DateTime.Now,
                AssetTagNumber = asset.AssetTagNumber,
                AssetName = asset.AssetName,
                ExportOrgInfo = new OrganizationInfo
                {
                    Org2 = apply.Org2,
                    OrgId = apply.RequestOrgId,
                    OrgIdentifier = apply.RequestOrgIdentifier,
                    OrgNam = apply.RequestOrgNam
                },
                ImportOrgInfo = new OrganizationInfo
                {
                    Org2 = apply.Org2,
                    OrgId = apply.ExchangeOrgId,
                    OrgIdentifier = apply.ExchangeOrgIdentifier,
                    OrgNam = apply.ExchangeOrgNam
                },
                AuthorizeOrgInfo = new OrganizationInfo
                {
                    Org2 = apply.Org2,
                    OrgId = apply.TargetOrgId,
                    OrgIdentifier = apply.TargetOrgIdentifier,
                    OrgNam = apply.TargetOrgNam
                }
            };
            //保存到数据库
            _assetRepository.Update(asset);
            _assetExchangeRepository.Update(apply);
            await _assetDeployRepository.AddAsync(deploy);
        }
        /// <inheritdoc />
        /// <summary>
        /// 撤销资产申请
        /// </summary>
        /// <param name="apply"></param>
        /// <param name="handleMessage"></param>
        public void RevokeAssetApply(AssetApply apply, string handleMessage)
        {
            apply.Revoke(handleMessage);
            _assetApplyRepository.Update(apply);
        }
        /// <inheritdoc />
        /// <summary>
        /// 撤销资产交回
        /// </summary>
        /// <param name="asset"></param>
        /// <param name="apply"></param>
        /// <param name="handleMessage"></param>
        public void RevokeAssetReturn(Asset asset, AssetReturn apply, string handleMessage)
        {
            asset.RevokeReturn();
            apply.Revoke(handleMessage);
            _assetRepository.Update(asset);
            _assetReturnRepository.Update(apply);
        }
        /// <inheritdoc />
        /// <summary>
        /// 撤销资产调换
        /// </summary>
        /// <param name="asset"></param>
        /// <param name="apply"></param>
        /// <param name="handleMessage"></param>
        public void RevokeAssetExchange(Asset asset, AssetExchange apply, string handleMessage)
        {
            asset.RevokeExchange();
            apply.Revoke(handleMessage);
            _assetRepository.Update(asset);
            _assetExchangeRepository.Update(apply);
        }
        /// <inheritdoc />
        /// <summary>
        /// 删除资产申请
        /// </summary>
        /// <param name="apply"></param>
        /// <param name="handleMessage"></param>
        public void RemoveAssetApply(AssetApply apply, string handleMessage)
        {
            _assetApplyRepository.Remove(apply);
        }
        /// <inheritdoc />
        /// <summary>
        /// 删除资产交回
        /// </summary>
        /// <param name="asset"></param>
        /// <param name="apply"></param>
        /// <param name="handleMessage"></param>
        public void RemoveAssetReturn(Asset asset, AssetReturn apply, string handleMessage)
        {
            asset.RemoveReturn();
            _assetRepository.Update(asset);
            _assetReturnRepository.Remove(apply);
        }
        /// <inheritdoc />
        /// <summary>
        /// 删除资产调换
        /// </summary>
        /// <param name="asset"></param>
        /// <param name="apply"></param>
        /// <param name="handleMessage"></param>
        public void RemoveAssetExchange(Asset asset, AssetExchange apply, string handleMessage)
        {
            asset.RemoveExchange();
            _assetRepository.Update(asset);
            _assetExchangeRepository.Remove(apply);
        }
        /// <inheritdoc />
        /// <summary>
        /// 创建一个资产申请
        /// </summary>
        /// <param name="user"></param>
        /// <param name="targetOrg"></param>
        /// <param name="assetCategoryId"></param>
        /// <param name="thirdLevelCategory"></param>
        /// <param name="message"></param>
        /// <returns></returns>
        public async Task<AssetApply> CreateAssetApply(IUser user, Organization targetOrg, Guid assetCategoryId, string thirdLevelCategory, string message)
        {
            var assetApply = new AssetApply(user,
                targetOrg,
                assetCategoryId,
                thirdLevelCategory,
                message);
            return await _assetApplyRepository.AddAsync(assetApply);
        }
        /// <inheritdoc />
        /// <summary>
        /// 创建资产交回
        /// </summary>
        /// <param name="asset"></param>
        /// <param name="user"></param>
        /// <param name="targetOrg"></param>
        /// <param name="message"></param>
        /// <returns></returns>
        public async Task<AssetReturn> CreateAssetReturn(Asset asset, IUser user, Organization targetOrg, string message)
        {
            asset.ModifyAssetStatus(AssetStatus.在途);
            var assetReturn = new AssetReturn(user, targetOrg, asset.Id, asset.AssetName, message);
            _assetRepository.Update(asset);
            return await _assetReturnRepository.AddAsync(assetReturn);
        }

        /// <inheritdoc />
        /// <summary>
        /// 创建一个资产调换
        /// </summary>
        /// <param name="asset"></param>
        /// <param name="user"></param>
        /// <param name="targetOrg"></param>
        /// <param name="exchangeOrg"></param>
        /// <param name="message"></param>
        /// <returns></returns>
        public async Task<AssetExchange> CreateAssetExchange(Asset asset, IUser user, Organization targetOrg,
            Organization exchangeOrg, string message)
        {
            asset.ModifyAssetStatus(AssetStatus.在途);
            var assetExchange = new AssetExchange(user, targetOrg, exchangeOrg, asset.Id, asset.AssetName,
                message);
            _assetRepository.Update(asset);
            return await _assetExchangeRepository.AddAsync(assetExchange);
        }
    }
}