using Boc.Assets.Domain.Core.SharedKernel;
using Boc.Assets.Domain.Models;
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
            asset.StatusChanged(AssetStatus.在库);
            asset.LastModifyDateTime = DateTime.Now;
            asset.LatestDeployRecord = $"从【{asset.StoredOrgIdentifier}】到【{apply.TargetOrgIdentifier}】";
            asset.AssetLocation = "暂无";
            asset.StoredOrgIdentifier = apply.TargetOrgIdentifier;
            asset.StoredOrgName = apply.TargetOrgNam;
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
                AssetId = asset.Id,
                AssetNo = asset.AssetNo,
                Org2 = apply.Org2,
                ExportOrgInfo = new OrganizationInfo()
                {
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
            asset.StatusChanged(AssetStatus.在用);
            asset.LastModifyDateTime = DateTime.Now;
            asset.LatestDeployRecord = $"从【{asset.StoredOrgIdentifier}】到【{apply.RequestOrgIdentifier}】";
            asset.AssetLocation = "暂无";
            asset.StoredOrgIdentifier = apply.RequestOrgIdentifier;
            asset.StoredOrgName = apply.RequestOrgNam;
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
                AssetId = asset.Id,
                AssetNo = asset.AssetNo,
                Org2 = apply.Org2,
                ExportOrgInfo = new OrganizationInfo
                {
                    OrgId = apply.TargetOrgId,
                    OrgIdentifier = apply.TargetOrgIdentifier,
                    OrgNam = apply.TargetOrgNam
                },
                ImportOrgInfo = new OrganizationInfo
                {
                    OrgId = apply.RequestOrgId,
                    OrgIdentifier = apply.RequestOrgIdentifier,
                    OrgNam = apply.RequestOrgNam
                },
                AuthorizeOrgInfo = new OrganizationInfo
                {
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
            asset.StatusChanged(AssetStatus.在用);
            asset.LastModifyDateTime = DateTime.Now;
            asset.LatestDeployRecord = $"从【{asset.StoredOrgIdentifier}】到【{apply.ExchangeOrgIdentifier}】";
            asset.AssetLocation = "暂无";
            asset.StoredOrgIdentifier = apply.ExchangeOrgIdentifier;
            asset.StoredOrgName = apply.ExchangeOrgNam;
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
                AssetId = asset.Id,
                AssetNo = asset.AssetNo,
                Org2 = apply.Org2,
                ExportOrgInfo = new OrganizationInfo
                {
                    OrgId = apply.RequestOrgId,
                    OrgIdentifier = apply.RequestOrgIdentifier,
                    OrgNam = apply.RequestOrgNam
                },
                ImportOrgInfo = new OrganizationInfo
                {
                    OrgId = apply.ExchangeOrgId,
                    OrgIdentifier = apply.ExchangeOrgIdentifier,
                    OrgNam = apply.ExchangeOrgNam
                },
                AuthorizeOrgInfo = new OrganizationInfo
                {
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
        /// <summary>
        /// 撤销资产交回
        /// </summary>
        /// <param name="apply"></param>
        /// <param name="handleMessage"></param>
        public void RevokeAssetReturn(AssetReturn apply, string handleMessage)
        {
            apply.Revoke(handleMessage);
            _assetReturnRepository.Update(apply);
        }
        /// <summary>
        /// 撤销资产调换
        /// </summary>
        /// <param name="apply"></param>
        /// <param name="handleMessage"></param>
        public void RevokeAssetExchange(AssetExchange apply, string handleMessage)
        {
            apply.Revoke(handleMessage);
            _assetExchangeRepository.Update(apply);
        }
        /// <summary>
        /// 删除资产申请
        /// </summary>
        /// <param name="apply"></param>
        public void RemoveAssetApply(AssetApply apply)
        {
            _assetApplyRepository.Remove(apply);
        }
        /// <summary>
        /// 删除资产交回
        /// </summary>
        /// <param name="asset"></param>
        /// <param name="apply"></param>
        public void RemoveAssetReturn(Asset asset, AssetReturn apply)
        {
            //如果这条申请的状态已经是完成状态，那么就直接删除，不用调整资产的状态了。
            if (apply.Status != AuditEntityStatus.已完成)
            {
                asset.StatusChanged(AssetStatus.在用);
                _assetRepository.Update(asset);
            }
            _assetReturnRepository.Remove(apply);
        }
        /// <summary>
        /// 删除资产调换
        /// </summary>
        /// <param name="asset"></param>
        /// <param name="apply"></param>
        public void RemoveAssetExchange(Asset asset, AssetExchange apply)
        {
            //如果这条申请的状态已经是完成状态，那么就直接删除，不用调整资产的状态了。
            if (apply.Status != AuditEntityStatus.已完成)
            {
                asset.StatusChanged(AssetStatus.在用);
                _assetRepository.Update(asset);
            }
            _assetExchangeRepository.Remove(apply);
        }
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
            asset.StatusChanged(AssetStatus.在途);
            var assetReturn = new AssetReturn(user, targetOrg, asset.Id, asset.AssetName, message);
            _assetRepository.Update(asset);
            return await _assetReturnRepository.AddAsync(assetReturn);
        }

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
            asset.StatusChanged(AssetStatus.在途);
            var assetExchange = new AssetExchange(user, targetOrg, exchangeOrg, asset.Id, asset.AssetName,
                message);
            _assetRepository.Update(asset);
            return await _assetExchangeRepository.AddAsync(assetExchange);
        }
    }
}