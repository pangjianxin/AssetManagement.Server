using Boc.Assets.Domain.Commands.Assets;
using Boc.Assets.Domain.Core.Bus;
using Boc.Assets.Domain.Core.Notifications;
using Boc.Assets.Domain.Core.SharedKernel;
using Boc.Assets.Domain.Events.Assets;
using Boc.Assets.Domain.Models.Assets;
using Boc.Assets.Domain.Repositories;
using Boc.Assets.Domain.Services;
using MediatR;
using System.Threading;
using System.Threading.Tasks;

namespace Boc.Assets.Domain.CommandHandlers.Assets
{
    public class AssetExchangeCommandHandler : CommandHandler,
        IRequestHandler<ExchangeAssetCommand, bool>,
        IRequestHandler<HandleAssetExchangeCommand, bool>,
        IRequestHandler<RevokeAssetExchangeCommand, bool>,
        IRequestHandler<RemoveAssetExchangeCommand, bool>
    {
        private readonly IOrganizationRepository _organizationRepository;
        private readonly IAssetRepository _assetRepository;
        private readonly IAssetExchangeRepository _assetExchangeRepository;
        private readonly IAssetDeployRepository _assetDeployRepository;
        private readonly IAssetDomainService _assetDomainService;
        private readonly IUser _user;

        public AssetExchangeCommandHandler(
            IUnitOfWork unitOfWork,
            IBus bus,
            INotificationHandler<DomainNotification> notifications,
            IOrganizationRepository organizationRepository,
            IAssetRepository assetRepository,
            IAssetExchangeRepository assetExchangeRepository,
            IAssetDeployRepository assetDeployRepository,
            IAssetDomainService assetDomainService,
            IUser user) : base(unitOfWork, bus, notifications)
        {
            _organizationRepository = organizationRepository;
            _assetRepository = assetRepository;
            _assetExchangeRepository = assetExchangeRepository;
            _assetDeployRepository = assetDeployRepository;
            _assetDomainService = assetDomainService;
            _user = user;
        }
        public async Task<bool> Handle(ExchangeAssetCommand request, CancellationToken cancellationToken)
        {
            if (!request.IsValid())
            {
                await NotifyValidationErrors(request);
                return false;
            }
            //查找调换机构是否存在
            var exchangeOrg = await _organizationRepository.GetByIdAsync(request.ExchangeOrgId);
            if (exchangeOrg == null)
            {
                await Bus.RaiseEventAsync(new DomainNotification("参数错误", "传入的调换机构参数有误，请联系管理员"));
                return false;
            }
            //查找授权机构是否存在
            var targetOrg = await _organizationRepository.GetByIdAsync(request.TargetOrgId);
            if (targetOrg == null)
            {
                await Bus.RaiseEventAsync(new DomainNotification("参数错误", "传入的审核机构参数有误，请联系管理员"));
                return false;
            }
            //查找调配的资产是否存在且符合调配的规则
            var asset = await _assetRepository.GetByIdAsync(request.AssetId);
            if (asset == null)
            {
                await Bus.RaiseEventAsync(new DomainNotification("参数错误", "传入的资产参数有误，请联系管理员"));
                return false;
            }
            if (asset.AssetStatus != AssetStatus.在用)
            {
                await Bus.RaiseEventAsync(new DomainNotification("状态错误", "选中的资产状态不为在用，不能进行该交易"));
                return false;
            }
            //如果备选资产符合调配规则那么继续
            var assetExchange = await _assetDomainService.CreateAssetExchange(asset, _user, targetOrg, exchangeOrg, request.Message);
            if (await CommitAsync())
            {
                //⑤生成一个资产申请调换的事件
                await Bus.RaiseEventAsync(new AssetExchangeNotifiedEvent(assetExchange, request.Message));
            }
            return true;
        }

        public async Task<bool> Handle(HandleAssetExchangeCommand request, CancellationToken cancellationToken)
        {
            if (!request.IsValid())
            {
                await NotifyValidationErrors(request);
                return false;
            }
            //查看事件是否存在
            var assetExchange = await _assetExchangeRepository.GetByIdAsync(request.EventId);
            if (assetExchange == null)
            {
                await Bus.RaiseEventAsync(new DomainNotification("参数错误", "传入的事件参数有误，请联系管理员"));
                return false;
            }
            //查看资产是否存在
            var asset = await _assetRepository.GetByIdAsync(assetExchange.AssetId);
            if (asset == null)
            {
                await Bus.RaiseEventAsync(new DomainNotification("参数错误", "传入的事件参数有误-没有找到相应资产，请联系管理员"));
                return false;
            }
            //如果事件和资产都存在，那么继续
            //①处理资产调配并返回调配记录
            await _assetDomainService.HandleAssetExchange(asset, assetExchange, request.Message);
            if (await CommitAsync())
            {
                //②发送一个资产调配已处理的事件
                await Bus.RaiseEventAsync(new AssetExchangeHandledEvent(assetExchange, request.Message));
                return true;
            }
            return false;
        }
        public async Task<bool> Handle(RevokeAssetExchangeCommand request, CancellationToken cancellationToken)
        {
            if (!request.IsValid())
            {
                await NotifyValidationErrors(request);
                return false;
            }
            //查看事件是否存在
            var assetExchange = await _assetExchangeRepository.GetByIdAsync(request.EventId);
            if (assetExchange == null)
            {
                await Bus.RaiseEventAsync(new DomainNotification("参数错误", "传入的事件参数有误，没有找到对应的事件，请联系管理员"));
                return false;
            }
            //查看资产是否存在
            var asset = await _assetRepository.GetByIdAsync(assetExchange.AssetId);
            if (asset == null)
            {
                await Bus.RaiseEventAsync(new DomainNotification("系统错误", "未找到相关资产，请联系管理员"));
                return false;
            }
            //如果上述满足，那么将该条资产的状态复原
            _assetDomainService.RemoveAssetExchange(asset, assetExchange, request.Message);
            if (await CommitAsync())
            {
                //然后发送资产调配事件撤销的事件以供后续处理
                await Bus.RaiseEventAsync(new AssetExchangeRevokedEvent(assetExchange, request.Message));
                return true;
            }
            return false;
        }

        public async Task<bool> Handle(RemoveAssetExchangeCommand request, CancellationToken cancellationToken)
        {
            if (!request.IsValid())
            {
                await NotifyValidationErrors(request);
                return false;
            }
            //查看事件是否存在
            var assetExchange = await _assetExchangeRepository.GetByIdAsync(request.EventId);
            if (assetExchange == null)
            {
                await Bus.RaiseEventAsync(new DomainNotification("参数错误", "传入的事件参数有误，没有找到对应的事件，请联系管理员"));
                return false;
            }
            //查看资产是否存在
            var asset = await _assetRepository.GetByIdAsync(assetExchange.AssetId);
            if (asset == null)
            {
                await Bus.RaiseEventAsync(new DomainNotification("系统错误", "未找到相关资产，请联系管理员"));
                return false;
            }
            //如果上述满足，那么将该条资产的状态复原
            _assetDomainService.RemoveAssetExchange(asset, assetExchange, request.Message);
            if (await CommitAsync())
            {
                //然后发送资产调配事件撤销的事件以供后续处理
                await Bus.RaiseEventAsync(new AssetExchangeRevokedEvent(assetExchange, request.Message));
                return true;
            }
            return false;
        }
    }
}