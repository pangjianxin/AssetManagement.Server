using Boc.Assets.Domain.Events.Assets;
using Boc.Assets.Domain.SignalR;
using MediatR;
using Microsoft.AspNetCore.SignalR;
using System;
using System.Globalization;
using System.Threading;
using System.Threading.Tasks;

namespace Boc.Assets.Domain.EventsHandler
{
    public class AssetEventsHandler :
        INotificationHandler<AssetApplyCreatedEvent>,
        INotificationHandler<AssetApplyHandledEvent>,
        INotificationHandler<AssetApplyRevokedEvent>,
        INotificationHandler<AssetApplyRemovedEvent>,
        INotificationHandler<AssetReturnCreatedEvent>,
        INotificationHandler<AssetReturnHandledEvent>,
        INotificationHandler<AssetReturnRevokedEvent>,
        INotificationHandler<AssetReturnRemovedEvent>,
        INotificationHandler<AssetExchangeCreatedEvent>,
        INotificationHandler<AssetExchangeHandledEvent>,
        INotificationHandler<AssetExchangeRevokedEvent>,
        INotificationHandler<AssetExchangeRemovedEvent>
    {

        private readonly IHubContext<EventMessageHub, IEventNotification> _hubContext;

        public AssetEventsHandler(IHubContext<EventMessageHub, IEventNotification> hubContext)
        {
            _hubContext = hubContext;
        }
        #region assetApply
        /// <summary>
        /// 资产申请发送已接受的消通知
        /// </summary>
        /// <param name="notification"></param>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        public async Task Handle(AssetApplyCreatedEvent notification, CancellationToken cancellationToken)
        {
            await _hubContext.Clients.User(notification.RequestOrgIdentifier).Notify(notification.TargetOrgIdentifier,
                DateTime.Now.ToLocalTime().ToString(CultureInfo.InvariantCulture),
                $"{notification}已送达");
            await _hubContext.Clients.User(notification.TargetOrgIdentifier).Notify(notification.RequestOrgIdentifier,
                DateTime.Now.ToLocalTime().ToString(CultureInfo.InvariantCulture),
                $"发起了{notification}");
        }
        /// <summary>
        /// 资产申请已处理发送的通知
        /// </summary>
        /// <param name="notification"></param>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>

        public async Task Handle(AssetApplyHandledEvent notification, CancellationToken cancellationToken)
        {

            await _hubContext.Clients.User(notification.RequestOrgIdentifier).Notify(notification.TargetOrgIdentifier,
                DateTime.Now.ToLocalTime().ToString(CultureInfo.InvariantCulture),
                $"资产申请已通过审核");
        }
        /// <summary>
        /// 资产申请撤销通知
        /// </summary>
        /// <param name="notification"></param>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        public async Task Handle(AssetApplyRevokedEvent notification, CancellationToken cancellationToken)
        {

            await _hubContext.Clients.User(notification.RequestOrgIdentifier).Notify(notification.TargetOrgIdentifier,
                DateTime.Now.ToLocalTime().ToString(CultureInfo.InvariantCulture),
                $"资产申请没有通过审核");
        }
        /// <summary>
        /// 资产申请删除的通知
        /// </summary>
        /// <param name="notification"></param>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        public async Task Handle(AssetApplyRemovedEvent notification, CancellationToken cancellationToken)
        {
            await _hubContext.Clients.User(notification.RequestOrgIdentifier).Notify(notification.TargetOrgIdentifier,
                DateTime.Now.ToLocalTime().ToString(CultureInfo.InvariantCulture),
                $"资产申请已删除");
        }
        #endregion
        #region assetReturn
        /// <summary>
        /// 资产交回通知
        /// </summary>
        /// <param name="notification"></param>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        public async Task Handle(AssetReturnCreatedEvent notification, CancellationToken cancellationToken)
        {
            await _hubContext.Clients.User(notification.RequestOrgIdentifier).Notify(notification.TargetOrgIdentifier,
                DateTime.Now.ToLocalTime().ToString(CultureInfo.InvariantCulture),
                $"{notification}申请已送达");
            await _hubContext.Clients.User(notification.TargetOrgIdentifier).Notify(notification.RequestOrgIdentifier,
                DateTime.Now.ToLocalTime().ToString(CultureInfo.InvariantCulture),
                $"发起了{notification}");
        }
        /// <summary>
        /// 资产交回已处理的通知
        /// </summary>
        /// <param name="notification"></param>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        public async Task Handle(AssetReturnHandledEvent notification, CancellationToken cancellationToken)
        {

            await _hubContext.Clients.User(notification.RequestOrgIdentifier).Notify(notification.TargetOrgIdentifier,
                DateTime.Now.ToLocalTime().ToString(CultureInfo.InvariantCulture),
                $"资产交回申请已通过审核");
        }
        /// <summary>
        /// 资产交回撤销的通知
        /// </summary>
        /// <param name="notification"></param>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        public async Task Handle(AssetReturnRevokedEvent notification, CancellationToken cancellationToken)
        {

            await _hubContext.Clients.User(notification.RequestOrgIdentifier).Notify(notification.TargetOrgIdentifier,
                DateTime.Now.ToLocalTime().ToString(CultureInfo.InvariantCulture),
                $"你的资产交回没有通过审核");
        }
        /// <summary>
        /// 资产交回已删除的通知
        /// </summary>
        /// <param name="notification"></param>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        public async Task Handle(AssetExchangeRemovedEvent notification, CancellationToken cancellationToken)
        {
            await _hubContext.Clients.User(notification.RequestOrgIdentifier).Notify(notification.TargetOrgIdentifier,
                DateTime.Now.ToLocalTime().ToString(CultureInfo.InvariantCulture),
                $"资产调换申请已删除");
        }
        #endregion
        #region assetExchange
        /// <summary>
        /// 资产交换发送的通知
        /// </summary>
        /// <param name="notification"></param>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        public async Task Handle(AssetExchangeCreatedEvent notification, CancellationToken cancellationToken)
        {
            await _hubContext.Clients.User(notification.RequestOrgIdentifier).Notify(notification.TargetOrgIdentifier,
                DateTime.Now.ToLocalTime().ToString(CultureInfo.InvariantCulture),
                $"{notification}申请已送达");
            await _hubContext.Clients.User(notification.TargetOrgIdentifier).Notify(notification.RequestOrgIdentifier,
                DateTime.Now.ToLocalTime().ToString(CultureInfo.InvariantCulture),
                $"发起了{notification}");
        }
        /// <summary>
        /// 资产调换完成的通知
        /// </summary>
        /// <param name="notification"></param>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        public async Task Handle(AssetExchangeHandledEvent notification, CancellationToken cancellationToken)
        {
            await _hubContext.Clients.User(notification.RequestOrgIdentifier).Notify(notification.TargetOrgIdentifier,
                DateTime.Now.ToLocalTime().ToString(CultureInfo.InvariantCulture),
                $"资产调换已通过审核");
        }
        /// <summary>
        /// 资产交回删除的通知
        /// </summary>
        /// <param name="notification"></param>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        public async Task Handle(AssetReturnRemovedEvent notification, CancellationToken cancellationToken)
        {
            await _hubContext.Clients.User(notification.RequestOrgIdentifier).Notify(notification.TargetOrgIdentifier,
                DateTime.Now.ToLocalTime().ToString(CultureInfo.InvariantCulture),
                $"资产交回申请已删除");
        }
        /// <summary>
        /// 资产调换撤销的通知
        /// </summary>
        /// <param name="notification"></param>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        public async Task Handle(AssetExchangeRevokedEvent notification, CancellationToken cancellationToken)
        {
            await _hubContext.Clients.User(notification.RequestOrgIdentifier).Notify(notification.TargetOrgIdentifier,
                DateTime.Now.ToLocalTime().ToString(CultureInfo.InvariantCulture),
                $"资产机构间调换没有通过审核");
        }
        #endregion
    }
}