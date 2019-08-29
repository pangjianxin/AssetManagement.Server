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
    public class AssetApplyCommandHandler : CommandHandler,
        IRequestHandler<ApplyAssetCommand, bool>,
        IRequestHandler<HandleAssetApplyCommand, bool>,
        IRequestHandler<RevokeAssetApplyCommand, bool>,
        IRequestHandler<RemoveAssetApplyCommand, bool>
    {
        private readonly IAssetCategoryRepository _assetCategoryRepository;
        private readonly IOrganizationRepository _organizationRepository;
        private readonly IAssetRepository _assetRepository;
        private readonly IAssetApplyRepository _assetApplyRepository;
        private readonly IAssetDeployRepository _assetDeployRepository;
        private readonly IAssetDomainService _assetDomainService;
        private readonly IUser _user;


        public AssetApplyCommandHandler(
            IUnitOfWork unitOfWork,
            IBus bus,
            INotificationHandler<DomainNotification> notifications,
            IAssetCategoryRepository assetCategoryRepository,
            IOrganizationRepository organizationRepository,
            IAssetRepository assetRepository,
            IAssetApplyRepository assetApplyRepository,
            IAssetDeployRepository assetDeployRepository,
            IAssetDomainService assetDomainService,
            IUser user) : base(unitOfWork, bus, notifications)
        {
            _assetCategoryRepository = assetCategoryRepository;
            _organizationRepository = organizationRepository;
            _assetRepository = assetRepository;
            _assetApplyRepository = assetApplyRepository;
            _assetDeployRepository = assetDeployRepository;
            _assetDomainService = assetDomainService;
            _user = user;
        }
        public async Task<bool> Handle(ApplyAssetCommand request, CancellationToken cancellationToken)
        {
            if (!request.IsValid())
            {
                await NotifyValidationErrors(request);
                return false;
            }

            var assetCategory = await _assetCategoryRepository.GetByIdAsync(request.AssetCategoryId);
            if (assetCategory == null)
            {
                await Bus.RaiseEventAsync(new DomainNotification("系统错误", "传入的资产分类参数有误，请联系管理员"));
                return false;
            }
            var targetOrg = await _organizationRepository.GetByIdAsync(request.TargetOrgId);
            if (targetOrg == null)
            {
                await Bus.RaiseEventAsync(new DomainNotification("系统错误", "查找目标机构信息为空，请联系管理员"));
                return false;
            }

            var assetApply = new AssetApply(_user,
                targetOrg,
                assetCategory.Id,
                assetCategory.AssetThirdLevelCategory,
                request.Message);
            await _assetApplyRepository.AddAsync(assetApply);
            if (await CommitAsync())
            {
                await Bus.RaiseEventAsync(new AssetApplyEvent(assetApply));
                return true;
            }
            return false;
        }
        public async Task<bool> Handle(HandleAssetApplyCommand request, CancellationToken cancellationToken)
        {
            if (!request.IsValid())
            {
                await NotifyValidationErrors(request);
                return false;
            }

            var asset = await _assetRepository.GetByIdAsync(request.AssetId);
            if (asset == null)
            {
                await Bus.RaiseEventAsync(new DomainNotification("参数错误", "传入的资产参数有误，请联系管理员"));
                return false;
            }

            if (asset.AssetStatus != AssetStatus.在库)
            {
                await Bus.RaiseEventAsync(new DomainNotification("状态错误", "传入的资产状态不为在库，不能分配该资产"));
                return false;
            }

            var assetApply = await _assetApplyRepository.GetByIdAsync(request.EventId);
            if (assetApply == null)
            {
                await Bus.RaiseEventAsync(new DomainNotification("参数错误", "传入的事件参数有误，请联系管理员"));
                return false;
            }

            if (assetApply.Status != AuditEntityStatus.待处理)
            {
                await Bus.RaiseEventAsync(new DomainNotification("状态错误", "事件状态不为待处理，不能处理该事件，请联系管理员"));
                return false;
            }
            //资产申请返回一个deploy对象
            var deploy = _assetDomainService.HandleAssetApplying(asset, assetApply);
            //资产申请后该资产状态变为在用
            _assetRepository.Update(asset);
            //将资产申请返回的deploy对象持久化到数据库
            await _assetDeployRepository.AddAsync(deploy);
            if (await CommitAsync())
            {
                //所有步骤处理完成后该资产申请事件的状态要变更为完成，该项操作由一个资产申请事件完成状态/事件来进行处理
                //因为要考虑后期发送signalR什么的操作，所以这里要做一下业务解耦
                await Bus.RaiseEventAsync(new AssetApplyHandledEvent(assetApply));
                return true;
            }
            return false;
        }
        public async Task<bool> Handle(RevokeAssetApplyCommand request, CancellationToken cancellationToken)
        {
            if (!request.IsValid())
            {
                await NotifyValidationErrors(request);
                return false;
            }
            var assetApply = await _assetApplyRepository.GetByIdAsync(request.EventId);
            if (assetApply == null)
            {
                await Bus.RaiseEventAsync(new DomainNotification("参数错误", "传入的事件参数有误，没有找到对应的事件，请联系管理员"));
                return false;
            }
            assetApply.Revoke(request.Message);
            _assetApplyRepository.Update(assetApply);
            if (await CommitAsync())
            {
                await Bus.RaiseEventAsync(new AssetApplyRevokedEvent(assetApply, request.Message));
                return true;
            }
            return false;
        }

        public async Task<bool> Handle(RemoveAssetApplyCommand request, CancellationToken cancellationToken)
        {
            var assetApply = await _assetApplyRepository.GetByIdAsync(request.EventId);
            if (assetApply == null)
            {
                await Bus.RaiseEventAsync(new DomainNotification("系统错误", "为找到指定的事件进行处理，请联系管理员"));
                return false;
            }
            assetApply = _assetApplyRepository.Remove(assetApply);
            if (await CommitAsync())
            {
                return true;
            }
            return false;
        }
    }
}