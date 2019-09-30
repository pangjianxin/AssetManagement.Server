using Boc.Assets.Domain.Models;
using Boc.Assets.Domain.Models.Applies;
using Boc.Assets.Domain.Models.Assets;
using Boc.Assets.Domain.Repositories;
using Boc.Assets.Domain.Services;
using Boc.Assets.Domain.ValueObjects;
using System;
using System.Threading.Tasks;

namespace Boc.Assets.Infrastructure.DomainServices
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
            asset.LastDeployRecord = $"从【{asset.OrgInUseName}】到【{apply.TargetOrgIdentifier}】";
            asset.AssetLocation = "暂无";
            //资产交回时资产的在用机构属性全部置为null
            asset.OrganizationInUseId = null;
            asset.OrgInUseIdentifier = null;
            asset.OrgInUseName = null;
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
                ExportOrgInfo = new OrganizationInfo(apply.RequestOrgId, apply.RequestOrgIdentifier, apply.RequestOrgNam),
                ImportOrgInfo = new OrganizationInfo(apply.TargetOrgId, apply.TargetOrgIdentifier, apply.TargetOrgNam),
                AuthorizeOrgInfo = new OrganizationInfo(apply.TargetOrgId, apply.TargetOrgIdentifier, apply.TargetOrgNam)
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
            asset.LastDeployRecord = $"从【{asset.OrgInUseName}】到【{apply.RequestOrgIdentifier}】";
            asset.AssetLocation = "暂无";
            //资产的在用信息
            asset.OrganizationInUseId = apply.RequestOrgId;
            asset.OrgInUseIdentifier = apply.RequestOrgIdentifier;
            asset.OrgInUseName = apply.RequestOrgNam;
            //申请状态变化（变为已完成）
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
                ExportOrgInfo = new OrganizationInfo(apply.TargetOrgId, apply.TargetOrgIdentifier, apply.TargetOrgNam),
                ImportOrgInfo = new OrganizationInfo(apply.RequestOrgId, apply.RequestOrgIdentifier, apply.RequestOrgNam),
                AuthorizeOrgInfo = new OrganizationInfo(apply.TargetOrgId, apply.TargetOrgIdentifier, apply.TargetOrgNam)
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
            asset.LastDeployRecord = $"从【{asset.OrgInUseName}】到【{apply.ExchangeOrgIdentifier}】";
            asset.AssetLocation = "暂无";
            //资产的在用机构信息的变化
            asset.OrganizationInUseId = apply.ExchangeOrgId;
            asset.OrgInUseIdentifier = apply.ExchangeOrgIdentifier;
            asset.OrgInUseName = apply.ExchangeOrgNam;
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
                ExportOrgInfo = new OrganizationInfo(apply.RequestOrgId, apply.RequestOrgIdentifier, apply.RequestOrgNam),
                ImportOrgInfo = new OrganizationInfo(apply.ExchangeOrgId, apply.ExchangeOrgIdentifier, apply.ExchangeOrgNam),
                AuthorizeOrgInfo = new OrganizationInfo(apply.TargetOrgId, apply.TargetOrgIdentifier, apply.TargetOrgNam)
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
            //资产申请的撤销不会导致资产的任何变化
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
            //资产交回申请的撤销不会导致资产的任何变化
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
            //资产的机构间调换申请的撤销不会导致资产的任何变化
            apply.Revoke(handleMessage);
            _assetExchangeRepository.Update(apply);
        }
        /// <summary>
        /// 删除资产申请
        /// </summary>
        /// <param name="apply"></param>
        public void RemoveAssetApply(AssetApply apply)
        {
            //资产申请的删除不会导致资产的任何变化
            _assetApplyRepository.Remove(apply);
        }
        /// <summary>
        /// 删除资产交回。
        /// 删除资产交回时要考虑该条资产交回的状态，如为已完成，则应先将资产状态还原（目前是直接设置为在用），然后再进行删除
        /// </summary>
        /// <param name="asset"></param>
        /// <param name="apply"></param>
        public void RemoveAssetReturn(Asset asset, AssetReturn apply)
        {
            //删除一条资产交回申请时，要注意该条资产申请的状态是否为已完成，
            //因为一条已撤销的资产申请，对应的资产的状态很有可能还是在途
            //如果这条申请的状态已经是完成状态，那么就直接删除，不用调整资产的状态了。
            if (apply.Status != AuditEntityStatus.已完成)
            {
                asset.StatusChanged(AssetStatus.在用);
                _assetRepository.Update(asset);
            }
            _assetReturnRepository.Remove(apply);
        }
        /// <summary>
        /// 删除资产调换。逻辑同删除资产交回。
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
        /// <param name="principal"></param>
        /// <param name="targetOrg"></param>
        /// <param name="assetCategoryId"></param>
        /// <param name="thirdLevelCategory"></param>
        /// <param name="message"></param>
        /// <returns></returns>
        public async Task<AssetApply> CreateAssetApply(OrganizationInfo principal, OrganizationInfo targetOrg, Guid assetCategoryId, string thirdLevelCategory, string message)
        {
            var assetApply = new AssetApply(principal,
                targetOrg,
                message,
                assetCategoryId,
                thirdLevelCategory);
            return await _assetApplyRepository.AddAsync(assetApply);
        }

        /// <summary>
        /// 创建资产交回
        /// </summary>
        /// <param name="asset"></param>
        /// <param name="principal"></param>
        /// <param name="targetOrg"></param>
        /// <param name="message"></param>
        /// <returns></returns>
        public async Task<AssetReturn> CreateAssetReturn(Asset asset, OrganizationInfo principal, OrganizationInfo targetOrg, string message)
        {
            asset.StatusChanged(AssetStatus.在途);
            var assetReturn = new AssetReturn(principal, targetOrg, message, asset.Id, asset.AssetName);
            _assetRepository.Update(asset);
            return await _assetReturnRepository.AddAsync(assetReturn);
        }

        /// <summary>
        /// 创建一个资产调换
        /// </summary>
        /// <param name="asset"></param>
        /// <param name="principal"></param>
        /// <param name="targetOrg"></param>
        /// <param name="exchangeOrg"></param>
        /// <param name="message"></param>
        /// <returns></returns>
        public async Task<AssetExchange> CreateAssetExchange(Asset asset, OrganizationInfo principal, OrganizationInfo targetOrg,
            OrganizationInfo exchangeOrg, string message)
        {
            asset.StatusChanged(AssetStatus.在途);
            var assetExchange = new AssetExchange(principal, targetOrg, exchangeOrg, message, asset.Id, asset.AssetName);
            _assetRepository.Update(asset);
            return await _assetExchangeRepository.AddAsync(assetExchange);
        }
    }
}