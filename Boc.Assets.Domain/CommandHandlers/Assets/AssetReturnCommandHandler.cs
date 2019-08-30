using Boc.Assets.Domain.Commands.Assets;
using Boc.Assets.Domain.Core.Bus;
using Boc.Assets.Domain.Core.Notifications;
using Boc.Assets.Domain.Core.SharedKernel;
using Boc.Assets.Domain.Events.Assets;
using Boc.Assets.Domain.Models;
using Boc.Assets.Domain.Models.Assets;
using Boc.Assets.Domain.Models.Assets.Audit;
using Boc.Assets.Domain.Repositories;
using Boc.Assets.Domain.Services;
using MediatR;
using System.Threading;
using System.Threading.Tasks;

namespace Boc.Assets.Domain.CommandHandlers.Assets
{
    public class AssetReturnCommandHandler : CommandHandler,
        IRequestHandler<ReturnAssetCommand, bool>,
        IRequestHandler<HandleAssetReturnCommand, bool>,
        IRequestHandler<RevokeAssetReturnCommand, bool>,
        IRequestHandler<RemoveAssetReturnCommand, bool>
    {
        private readonly IAssetRepository _assetRepository;
        private readonly IOrganizationRepository _organizationRepository;
        private readonly IAssetReturnRepository _assetReturnRepository;
        private readonly IAssetDeployRepository _assetDeployRepository;
        private readonly IAssetDomainService _assetDomainService;
        private readonly IUser _user;

        public AssetReturnCommandHandler(
            IUnitOfWork unitOfWork,
            IBus bus,
            INotificationHandler<DomainNotification> notifications,
            IAssetRepository assetRepository,
            IOrganizationRepository organizationRepository,
            IAssetReturnRepository assetReturnRepository,
            IAssetDeployRepository assetDeployRepository,
            IAssetDomainService assetDomainService,
            IUser user) : base(unitOfWork, bus, notifications)
        {
            _assetRepository = assetRepository;
            _organizationRepository = organizationRepository;
            _assetReturnRepository = assetReturnRepository;
            _assetDeployRepository = assetDeployRepository;
            _assetDomainService = assetDomainService;
            _user = user;
        }
        public async Task<bool> Handle(ReturnAssetCommand request, CancellationToken cancellationToken)
        {
            if (!request.IsValid())
            {
                await NotifyValidationErrors(request);
                return false;
            }
            var targetAsset = await _assetRepository.GetByIdAsync(request.AssetId);
            if (targetAsset == null)
            {
                await Bus.RaiseEventAsync(new DomainNotification("系统错误", "传入的资产序号参数有误，请联系管理员"));
                return false;
            }
            if (targetAsset.AssetStatus == AssetStatus.在途)
            {
                await Bus.RaiseEventAsync(new DomainNotification("状态错误", $"该资产状态为{AssetStatus.在途.ToString()},请勿重复提交"));
                return false;
            }
            var targetOrg = await _organizationRepository.GetByIdAsync(request.TargetOrgId);
            if (targetOrg == null)
            {
                await Bus.RaiseEventAsync(new DomainNotification("系统错误", "传入的机构序号参数有误，请联系管理员"));
                return false;
            }
            targetAsset.ModifyAssetStatus(AssetStatus.在途);
            _assetRepository.Update(targetAsset);
            var assetReturn = new AssetReturn(_user, targetOrg, targetAsset.Id, targetAsset.AssetName, request.Message);
            await _assetReturnRepository.AddAsync(assetReturn);
            if (await CommitAsync())
            {
                await Bus.RaiseEventAsync(new AssetReturnNotifiedEvent(assetReturn, request.Message));
                return true;
            }
            return false;
        }
        public async Task<bool> Handle(HandleAssetReturnCommand request, CancellationToken cancellationToken)
        {
            if (!request.IsValid())
            {
                await NotifyValidationErrors(request);
                return false;
            }
            var assetReturn = await _assetReturnRepository.GetByIdAsync(request.EventId);
            if (assetReturn == null)
            {
                await Bus.RaiseEventAsync(new DomainNotification("参数错误", "传入的事件参数有误，请联系管理员"));
                return false;
            }
            if (assetReturn.Status != AuditEntityStatus.待处理)
            {
                await Bus.RaiseEventAsync(new DomainNotification("状态错误", "事件状态不为待处理，不能处理该事件，请联系管理员"));
                return false;
            }
            var asset = await _assetRepository.GetByIdAsync(assetReturn.AssetId);
            if (asset.AssetStatus != AssetStatus.在途)
            {
                await Bus.RaiseEventAsync(new DomainNotification("状态错误", "传入的资产状态不为在途，不能接收该资产，请核对"));
                return false;
            }
            var deploy = _assetDomainService.HandleAssetReturn(asset, assetReturn);
            _assetRepository.Update(asset);
            await _assetDeployRepository.AddAsync(deploy);
            if (await CommitAsync())
            {
                await Bus.RaiseEventAsync(new AssetReturnHandledEvent(assetReturn, request.Message));
                return true;
            }
            return false;
        }
        public async Task<bool> Handle(RevokeAssetReturnCommand request, CancellationToken cancellationToken)
        {
            if (!request.IsValid())
            {
                await NotifyValidationErrors(request);
                return false;
            }
            var assetReturn = await _assetReturnRepository.GetByIdAsync(request.EventId);
            if (assetReturn == null)
            {
                await Bus.RaiseEventAsync(new DomainNotification("参数错误", "传入的事件参数有误，没有找到对应的事件，请联系管理员"));
                return false;
            }
            var asset = await _assetRepository.GetByIdAsync(assetReturn.AssetId);
            //修改资产的状态，资产状态继续变更为在用
            asset.ModifyAssetStatus(AssetStatus.在用);
            //修改资产交回申请的状态，变更为已撤销
            assetReturn.Revoke(request.Message);
            //更新数据库中的状态
            _assetReturnRepository.Update(assetReturn);
            _assetRepository.Update(asset);
            //提交
            if (await CommitAsync())
            {
                await Bus.RaiseEventAsync(new AssetReturnRevokedEvent(assetReturn, request.Message));
                return true;
            }
            return false;
        }

        public async Task<bool> Handle(RemoveAssetReturnCommand request, CancellationToken cancellationToken)
        {
            if (!request.IsValid())
            {
                await NotifyValidationErrors(request);
                return false;
            }
            var assetReturn = await _assetReturnRepository.GetByIdAsync(request.EventId);
            if (assetReturn == null)
            {
                await Bus.RaiseEventAsync(new DomainNotification("参数错误", "传入的事件参数有误，没有找到对应的事件，请联系管理员"));
                return false;
            }
            var asset = await _assetRepository.GetByIdAsync(assetReturn.AssetId);
            asset.ModifyAssetStatus(AssetStatus.在用);
            _assetRepository.Update(asset);
            _assetReturnRepository.Remove(assetReturn);
            if (await CommitAsync())
            {
                await Bus.RaiseEventAsync(new AssetReturnRevokedEvent(assetReturn, request.Message));
                return true;
            }
            return false;
        }
    }
}