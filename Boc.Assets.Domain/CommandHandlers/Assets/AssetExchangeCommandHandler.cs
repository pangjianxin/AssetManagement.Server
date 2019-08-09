using Boc.Assets.Domain.Commands.Assets;
using Boc.Assets.Domain.Core.Bus;
using Boc.Assets.Domain.Core.Notifications;
using Boc.Assets.Domain.Core.SharedKernel;
using Boc.Assets.Domain.Events.Assets;
using Boc.Assets.Domain.Models.Assets;
using Boc.Assets.Domain.Models.Assets.Audit;
using Boc.Assets.Domain.Repositories;
using MediatR;
using System.Threading;
using System.Threading.Tasks;

namespace Boc.Assets.Domain.CommandHandlers.Assets
{
    public class AssetExchangeCommandHandler : CommandHandler,
        IRequestHandler<ExchangeAssetCommand, bool>,
        IRequestHandler<HandleAssetExchangeCommand, bool>,
        IRequestHandler<RevokeAssetExchangeCommand, bool>
    {
        private readonly IOrganizationRepository _organizationRepository;
        private readonly IAssetRepository _assetRepository;
        private readonly IAssetExchangeRepository _assetExchangeRepository;
        private readonly IAssetDeployRepository _assetDeployRepository;

        public AssetExchangeCommandHandler(
            IUnitOfWork unitOfWork,
            IBus bus,
            INotificationHandler<DomainNotification> notifications,
            IOrganizationRepository organizationRepository,
            IAssetRepository assetRepository,
            IAssetExchangeRepository assetExchangeRepository,
            IAssetDeployRepository assetDeployRepository) : base(unitOfWork, bus, notifications)
        {
            _organizationRepository = organizationRepository;
            _assetRepository = assetRepository;
            _assetExchangeRepository = assetExchangeRepository;
            _assetDeployRepository = assetDeployRepository;
        }
        public async Task<bool> Handle(ExchangeAssetCommand request, CancellationToken cancellationToken)
        {
            if (!await request.IsValid())
            {
                await NotifyValidationErrors(request);
                return false;
            }
            //查找调换机构是否存在
            var exchangeOrg = await _organizationRepository.GetByIdAsync(request.ExchangeOrgId);
            if (exchangeOrg == null)
            {
                await _bus.RaiseEventAsync(new DomainNotification("参数错误", "传入的调换机构参数有误，请联系管理员"));
                return false;
            }
            //查找授权机构是否存在
            var targetOrg = await _organizationRepository.GetByIdAsync(request.TargetOrgId);
            if (targetOrg == null)
            {
                await _bus.RaiseEventAsync(new DomainNotification("参数错误", "传入的审核机构参数有误，请联系管理员"));
                return false;
            }
            //查找调配的资产是否存在且符合调配的规则
            var asset = await _assetRepository.GetByIdAsync(request.AssetId);
            if (asset == null)
            {
                await _bus.RaiseEventAsync(new DomainNotification("参数错误", "传入的资产参数有误，请联系管理员"));
                return false;
            }
            if (asset.AssetStatus != AssetStatus.在用)
            {
                await _bus.RaiseEventAsync(new DomainNotification("状态错误", "选中的资产状态不为在用，不能进行该交易"));
                return false;
            }
            //如果备选资产符合调配规则那么继续
            //①修改资产状态为在途
            asset.ModifyAssetStatus(AssetStatus.在途);
            //②将修改状态后的资产持久化到数据库
            _assetRepository.Update(asset);
            //③创建一个AssetExchange实例
            var assetExchange = new AssetExchange(request.Principal, targetOrg, exchangeOrg, asset.Id, asset.AssetName,
                request.Message);
            //④将这个AssetExchange实例持久化到数据库
            await _assetExchangeRepository.AddAsync(assetExchange);
            if (await CommitAsync())
            {
                //⑤生成一个资产申请调换的事件
                await _bus.RaiseEventAsync(new AssetExchangeEvent(assetExchange));
            }
            return true;
        }

        public async Task<bool> Handle(HandleAssetExchangeCommand request, CancellationToken cancellationToken)
        {
            if (!await request.IsValid())
            {
                await NotifyValidationErrors(request);
                return false;
            }
            //查看事件是否存在
            var assetExchange = await _assetExchangeRepository.GetByIdAsync(request.EventId);
            if (assetExchange == null)
            {
                await _bus.RaiseEventAsync(new DomainNotification("参数错误", "传入的事件参数有误，请联系管理员"));
                return false;
            }
            //查看资产是否存在
            var asset = await _assetRepository.GetByIdAsync(assetExchange.AssetId);
            if (asset == null)
            {
                await _bus.RaiseEventAsync(new DomainNotification("参数错误", "传入的事件参数有误-没有找到相应资产，请联系管理员"));
                return false;
            }
            //如果事件和资产都存在，那么继续
            //①处理资产调配并返回调配记录
            var deploy = asset.HandleAssetExchanging(assetExchange);
            //②持久化这个记录
            _assetRepository.Update(asset);
            await _assetDeployRepository.AddAsync(deploy);
            if (await CommitAsync())
            {
                //③发送一个资产调配已处理的事件
                await _bus.RaiseEventAsync(new AssetExchangeHandledEvent(assetExchange));
                return true;
            }
            return false;
        }
        public async Task<bool> Handle(RevokeAssetExchangeCommand request, CancellationToken cancellationToken)
        {
            if (!await request.IsValid())
            {
                await NotifyValidationErrors(request);
                return false;
            }
            //查看事件是否存在
            var assetExchange = await _assetExchangeRepository.GetByIdAsync(request.EventId);
            if (assetExchange == null)
            {
                await _bus.RaiseEventAsync(new DomainNotification("参数错误", "传入的事件参数有误，没有找到对应的事件，请联系管理员"));
                return false;
            }
            //查看资产是否存在
            var asset = await _assetRepository.GetByIdAsync(assetExchange.AssetId);
            if (asset == null)
            {
                await _bus.RaiseEventAsync(new DomainNotification("系统错误", "未找到相关资产，请联系管理员"));
                return false;
            }
            //如果上述满足，那么将该条资产的状态复原
            asset.ModifyAssetStatus(AssetStatus.在用);
            if (await CommitAsync())
            {
                //然后发送资产调配事件撤销的事件以供后续处理
                await _bus.RaiseEventAsync(new AssetExchangeRevokedEvent(assetExchange, request.Message));
                return true;
            }
            return false;
        }
    }
}