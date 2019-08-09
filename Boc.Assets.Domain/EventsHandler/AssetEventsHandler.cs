using Boc.Assets.Domain.Events.Assets;
using Boc.Assets.Domain.Repositories;
using Boc.Assets.Domain.SignalR;
using MediatR;
using Microsoft.AspNetCore.SignalR;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace Boc.Assets.Domain.EventsHandler
{
    public class AssetEventsHandler : INotificationHandler<AssetApplyEvent>,
        INotificationHandler<AssetApplyHandledEvent>,
        INotificationHandler<AssetApplyRevokedEvent>,
        INotificationHandler<AssetReturnEvent>,
        INotificationHandler<AssetReturnHandledEvent>,
        INotificationHandler<AssetReturnRevokedEvent>,
        INotificationHandler<AssetExchangeEvent>,
        INotificationHandler<AssetExchangeHandledEvent>,
        INotificationHandler<AssetExchangeRevokedEvent>
    {
        private readonly IAssetApplyRepository _assetApplyRepository;
        private readonly IAssetReturnRepository _assetReturnRepository;
        private readonly IAssetExchangeRepository _assetExchangeRepository;
        private readonly IHubContext<EventMessageHub, IEventNotification> _hubContext;

        public AssetEventsHandler(IAssetApplyRepository assetApplyRepository,
            IAssetReturnRepository assetReturnRepository,
            IAssetExchangeRepository assetExchangeRepository,
            IHubContext<EventMessageHub, IEventNotification> hubContext)
        {
            _assetApplyRepository = assetApplyRepository;
            _assetReturnRepository = assetReturnRepository;
            _assetExchangeRepository = assetExchangeRepository;
            _hubContext = hubContext;
        }
        public async Task Handle(AssetApplyEvent notification, CancellationToken cancellationToken)
        {
            await _hubContext.Clients.User(notification.AssetApply.RequestOrgIdentifier).Notify(notification.AssetApply.TargetOrgNam,
                DateTime.Now.ToLocalTime().ToString(),
                $"{notification.ToString()}申请已送达");
            await _hubContext.Clients.User(notification.AssetApply.TargetOrgIdentifier).Notify(notification.AssetApply.RequestOrgNam,
                DateTime.Now.ToLocalTime().ToString(),
                $"发起了{notification.ToString()}");
        }

        public async Task Handle(AssetReturnEvent notification, CancellationToken cancellationToken)
        {
            await _hubContext.Clients.User(notification.AssetReturn.RequestOrgIdentifier).Notify(notification.AssetReturn.TargetOrgNam,
                DateTime.Now.ToLocalTime().ToString(),
                $"{notification.ToString()}申请已送达");
            await _hubContext.Clients.User(notification.AssetReturn.TargetOrgIdentifier).Notify(notification.AssetReturn.RequestOrgNam,
                DateTime.Now.ToLocalTime().ToString(),
                $"发起了{notification.ToString()}");
        }
        public async Task Handle(AssetExchangeEvent notification, CancellationToken cancellationToken)
        {
            await _hubContext.Clients.User(notification.AssetExchange.RequestOrgIdentifier).Notify(notification.AssetExchange.TargetOrgNam,
                DateTime.Now.ToLocalTime().ToString(),
                $"{notification.ToString()}申请已送达");
            await _hubContext.Clients.User(notification.AssetExchange.TargetOrgIdentifier).Notify(notification.AssetExchange.RequestOrgNam,
                DateTime.Now.ToLocalTime().ToString(),
                $"发起了{notification.ToString()}");
        }

        public async Task Handle(AssetApplyHandledEvent notification, CancellationToken cancellationToken)
        {

            await _hubContext.Clients.User(notification.AssetApply.RequestOrgIdentifier).Notify(notification.AssetApply.TargetOrgNam,
                DateTime.Now.ToLocalTime().ToString(),
                $"{notification.AssetApply.ToString()}已通过审核");
        }

        public async Task Handle(AssetReturnHandledEvent notification, CancellationToken cancellationToken)
        {

            await _hubContext.Clients.User(notification.AssetReturn.RequestOrgIdentifier).Notify(notification.AssetReturn.TargetOrgNam,
                DateTime.Now.ToLocalTime().ToString(),
                $"{notification.AssetReturn.ToString()}已通过审核");
        }

        public async Task Handle(AssetExchangeHandledEvent notification, CancellationToken cancellationToken)
        {
            await _hubContext.Clients.User(notification.AssetExchange.RequestOrgIdentifier).Notify(notification.AssetExchange.TargetOrgNam,
                DateTime.Now.ToLocalTime().ToString(),
                $"{notification.AssetExchange.ToString()}已通过审核");
        }

        public async Task Handle(AssetApplyRevokedEvent notification, CancellationToken cancellationToken)
        {

            await _hubContext.Clients.User(notification.AssetApply.RequestOrgIdentifier).Notify(notification.AssetApply.TargetOrgNam,
                DateTime.Now.ToLocalTime().ToString(),
                $"{notification.AssetApply.ToString()}没有通过审核");
        }

        public async Task Handle(AssetReturnRevokedEvent notification, CancellationToken cancellationToken)
        {

            await _hubContext.Clients.User(notification.AssetReturn.RequestOrgIdentifier).Notify(notification.AssetReturn.TargetOrgNam,
                DateTime.Now.ToLocalTime().ToString(),
                $"{notification.AssetReturn.ToString()}没有通过审核");
        }

        public async Task Handle(AssetExchangeRevokedEvent notification, CancellationToken cancellationToken)
        {
            await _hubContext.Clients.User(notification.AssetExchange.RequestOrgIdentifier).Notify(notification.AssetExchange.TargetOrgNam,
                DateTime.Now.ToLocalTime().ToString(),
                $"{notification.AssetExchange.ToString()}没有通过审核");
        }
    }
}