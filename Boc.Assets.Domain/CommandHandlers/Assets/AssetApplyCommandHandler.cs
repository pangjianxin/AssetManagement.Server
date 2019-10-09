using Boc.Assets.Domain.Commands.Assets;
using Boc.Assets.Domain.Core.Bus;
using Boc.Assets.Domain.Core.Notifications;
using Boc.Assets.Domain.Core.SharedKernel;
using Boc.Assets.Domain.Events.Assets;
using Boc.Assets.Domain.Models;
using Boc.Assets.Domain.Models.Assets;
using Boc.Assets.Domain.Repositories;
using Boc.Assets.Domain.Services;
using Boc.Assets.Domain.ValueObjects;
using MediatR;
using System.Threading;
using System.Threading.Tasks;

namespace Boc.Assets.Domain.CommandHandlers.Assets
{
    public class AssetApplyCommandHandler : CommandHandler,
        IRequestHandler<CreateAssetApplyCommand, bool>,
        IRequestHandler<HandleAssetApplyCommand, bool>,
        IRequestHandler<RevokeAssetApplyCommand, bool>,
        IRequestHandler<RemoveAssetApplyCommand, bool>
    {
        private readonly IAssetCategoryRepository _assetCategoryRepository;
        private readonly IOrganizationRepository _organizationRepository;
        private readonly IAssetRepository _assetRepository;
        private readonly IAssetApplyRepository _assetApplyRepository;
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
            IAssetDomainService assetDomainService,
            IUser user) : base(unitOfWork, bus, notifications)
        {
            _assetCategoryRepository = assetCategoryRepository;
            _organizationRepository = organizationRepository;
            _assetRepository = assetRepository;
            _assetApplyRepository = assetApplyRepository;
            _assetDomainService = assetDomainService;
            _user = user;
        }
        /// <summary>
        /// 新增一个资产申请（AssetApply）
        /// </summary>
        /// <param name="request"></param>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        public async Task<bool> Handle(CreateAssetApplyCommand request, CancellationToken cancellationToken)
        {
            if (!request.IsValid())
            {
                await NotifyValidationErrors(request);
                return false;
            }
            //首先找出对应的资产分类，然后判断一下
            var assetCategory = await _assetCategoryRepository.GetByIdAsync(request.AssetCategoryId);
            if (assetCategory == null)
            {
                await Bus.RaiseEventAsync(new DomainNotification("系统错误", "传入的资产分类参数有误，请联系管理员"));
                return false;
            }
            //然后找出目标机构信息，然后判断一下
            var targetOrg = await _organizationRepository.GetByIdAsync(request.TargetOrgId);
            if (targetOrg == null)
            {
                await Bus.RaiseEventAsync(new DomainNotification("系统错误", "查找目标机构信息为空，请联系管理员"));
                return false;
            }
            //然后传入资产的领域服务中。
            var principal = new OrganizationInfo(_user.OrgId, _user.OrgIdentifier, _user.OrgNam);
            var targetOrgInfo = targetOrg.GetValueObject();
            var assetApply = await _assetDomainService.CreateAssetApply(principal,
                targetOrgInfo,
                  assetCategory.Id,
                  assetCategory.AssetThirdLevelCategory,
                  request.Message);
            if (await CommitAsync())
            {
                await Bus.RaiseEventAsync(new AssetApplyCreatedEvent(
                    _user.OrgId,
                    assetApply,
                    request.Message));
                return true;
            }
            return false;
        }
        /// <summary>
        /// 处理资产申请
        /// </summary>
        /// <param name="request"></param>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
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
                await Bus.RaiseEventAsync(new DomainNotification("状态错误", "该资产状态不为在库，不能分配该资产"));
                return false;
            }

            var assetApply = await _assetApplyRepository.GetByIdAsync(request.ApplyId);
            if (assetApply == null)
            {
                await Bus.RaiseEventAsync(new DomainNotification("参数错误", "传入的事件参数有误，请联系管理员"));
                return false;
            }

            if (assetApply.Status != AuditEntityStatus.待处理)
            {
                await Bus.RaiseEventAsync(new DomainNotification("状态错误", "该申请已经处理过，请核实后再进行提交"));
                return false;
            }
            await _assetDomainService.HandleAssetApply(asset, assetApply, request.Message);
            if (await CommitAsync())
            {
                //所有步骤处理完成后该资产申请事件的状态要变更为完成，该项操作由一个资产申请事件完成状态/事件来进行处理
                //因为要考虑后期发送signalR什么的操作，所以这里要做一下业务解耦
                await Bus.RaiseEventAsync(new AssetApplyHandledEvent(
                    _user.OrgId,
                    assetApply,
                    request.Message));
                return true;
            }
            return false;
        }
        /// <summary>
        /// 撤销资产申请
        /// </summary>
        /// <param name="request"></param>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        public async Task<bool> Handle(RevokeAssetApplyCommand request, CancellationToken cancellationToken)
        {
            if (!request.IsValid())
            {
                await NotifyValidationErrors(request);
                return false;
            }
            var assetApply = await _assetApplyRepository.GetByIdAsync(request.ApplyId);
            if (assetApply == null)
            {
                await Bus.RaiseEventAsync(new DomainNotification("参数错误", "传入的事件参数有误，没有找到对应的事件，请联系管理员"));
                return false;
            }
            _assetDomainService.RevokeAssetApply(assetApply, request.Message);
            if (await CommitAsync())
            {
                await Bus.RaiseEventAsync(new AssetApplyRevokedEvent(
                    _user.OrgId,
                    assetApply,
                    request.Message));
                return true;
            }
            return false;
        }

        public async Task<bool> Handle(RemoveAssetApplyCommand request, CancellationToken cancellationToken)
        {
            if (!request.IsValid())
            {
                await Bus.RaiseEventAsync(new DomainNotification("客户端错误", "模型有效性验证未通过"));
                return false;
            }
            var assetApply = await _assetApplyRepository.GetByIdAsync(request.ApplyId);
            if (assetApply == null)
            {
                await Bus.RaiseEventAsync(new DomainNotification("系统错误", "为找到指定的事件进行处理，请联系管理员"));
                return false;
            }
            _assetDomainService.RemoveAssetApply(assetApply);
            if (await CommitAsync())
            {
                await Bus.RaiseEventAsync(new AssetApplyRemovedEvent(
                    _user.OrgId,
                    assetApply,
                    request.Message));
                return true;
            }
            return false;
        }
    }
}