using Boc.Assets.Domain.Events.Assets;
using Boc.Assets.Domain.EventsHandler.SignalR;
using MediatR;
using Microsoft.AspNetCore.SignalR;
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

        private readonly IHubContext<ChatHub> _hubContext;

        public AssetEventsHandler(IHubContext<ChatHub> hubContext)
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
        public Task Handle(AssetApplyCreatedEvent notification, CancellationToken cancellationToken)
        {
            // do nothing
            //await _hubContext.Clients.User(notification.RequestOrgIdentifier).Notify(notification.TargetOrgIdentifier,
            //    DateTime.Now.ToLocalTime().ToString(CultureInfo.InvariantCulture),
            //    $"资产申请已送至{notification.TargetOrgIdentifier}");
            //await _hubContext.Clients.User(notification.TargetOrgIdentifier).Notify(notification.RequestOrgIdentifier,
            //    DateTime.Now.ToLocalTime().ToString(CultureInfo.InvariantCulture),
            //    $"{notification.RequestOrgIdentifier}发起了资产申请");
            return Task.CompletedTask;
        }
        /// <summary>
        /// 资产申请已处理发送的通知
        /// </summary>
        /// <param name="notification"></param>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>

        public Task Handle(AssetApplyHandledEvent notification, CancellationToken cancellationToken)
        {
            //do nothing
            //await _hubContext.Clients.User(notification.RequestOrgIdentifier).Notify(notification.TargetOrgIdentifier,
            //    DateTime.Now.ToLocalTime().ToString(CultureInfo.InvariantCulture),
            //    $"你申请的{notification.AssetCategory}已通过审核");
            return Task.CompletedTask;
        }
        /// <summary>
        /// 资产申请撤销通知
        /// </summary>
        /// <param name="notification"></param>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        public Task Handle(AssetApplyRevokedEvent notification, CancellationToken cancellationToken)
        {

            //await _hubContext.Clients.User(notification.RequestOrgIdentifier).Notify(notification.TargetOrgIdentifier,
            //    DateTime.Now.ToLocalTime().ToString(CultureInfo.InvariantCulture),
            //    $"你申请的{notification.AssetCategory}没有通过审核");
            return Task.CompletedTask;
        }
        /// <summary>
        /// 资产申请删除的通知
        /// </summary>
        /// <param name="notification"></param>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        public Task Handle(AssetApplyRemovedEvent notification, CancellationToken cancellationToken)
        {
            //await _hubContext.Clients.User(notification.RequestOrgIdentifier).Notify(notification.TargetOrgIdentifier,
            //    DateTime.Now.ToLocalTime().ToString(CultureInfo.InvariantCulture),
            //    $"你的资产申请（{notification.AssetCategory}）已被删除");
            return Task.CompletedTask;
        }
        #endregion
        #region assetReturn
        /// <summary>
        /// 资产交回通知
        /// </summary>
        /// <param name="notification"></param>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        public Task Handle(AssetReturnCreatedEvent notification, CancellationToken cancellationToken)
        {
            //await _hubContext.Clients.User(notification.RequestOrgIdentifier).Notify(notification.TargetOrgIdentifier,
            //    DateTime.Now.ToLocalTime().ToString(CultureInfo.InvariantCulture),
            //    $"你的资产交回申请已送达{notification.TargetOrgIdentifier}");
            //await _hubContext.Clients.User(notification.TargetOrgIdentifier).Notify(notification.RequestOrgIdentifier,
            //    DateTime.Now.ToLocalTime().ToString(CultureInfo.InvariantCulture),
            //    $"{notification.RequestOrgIdentifier}申请交回{notification.AssetName}");
            return Task.CompletedTask;
        }
        /// <summary>
        /// 资产交回已处理的通知
        /// </summary>
        /// <param name="notification"></param>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        public Task Handle(AssetReturnHandledEvent notification, CancellationToken cancellationToken)
        {
            // do nothing
            //await _hubContext.Clients.User(notification.RequestOrgIdentifier).Notify(notification.TargetOrgIdentifier,
            //    DateTime.Now.ToLocalTime().ToString(CultureInfo.InvariantCulture),
            //    $"你交回{notification.AssetName}的申请已通过审核");
            return Task.CompletedTask;
        }
        /// <summary>
        /// 资产交回撤销的通知
        /// </summary>
        /// <param name="notification"></param>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        public Task Handle(AssetReturnRevokedEvent notification, CancellationToken cancellationToken)
        {
            //do nothing
            //await _hubContext.Clients.User(notification.RequestOrgIdentifier).Notify(notification.TargetOrgIdentifier,
            //    DateTime.Now.ToLocalTime().ToString(CultureInfo.InvariantCulture),
            //    $"你交回{notification.AssetName}的申请没有通过审核");
            return Task.CompletedTask;
        }
        /// <summary>
        /// 资产交回已删除的通知
        /// </summary>
        /// <param name="notification"></param>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        public Task Handle(AssetExchangeRemovedEvent notification, CancellationToken cancellationToken)
        {
            //await _hubContext.Clients.User(notification.RequestOrgIdentifier).Notify(notification.TargetOrgIdentifier,
            //    DateTime.Now.ToLocalTime().ToString(CultureInfo.InvariantCulture),
            //    $"你交回{notification.AssetName}的申请已删除");
            return Task.CompletedTask;
        }
        #endregion
        #region assetExchange
        /// <summary>
        /// 资产交换发送的通知
        /// </summary>
        /// <param name="notification"></param>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        public Task Handle(AssetExchangeCreatedEvent notification, CancellationToken cancellationToken)
        {
            //await _hubContext.Clients.User(notification.RequestOrgIdentifier).Notify(notification.TargetOrgIdentifier,
            //    DateTime.Now.ToLocalTime().ToString(CultureInfo.InvariantCulture),
            //    $"将{notification.AssetName}调换至{notification.ExchangeOrgIdentifier}的申请已送达");
            //await _hubContext.Clients.User(notification.TargetOrgIdentifier).Notify(notification.RequestOrgIdentifier,
            //    DateTime.Now.ToLocalTime().ToString(CultureInfo.InvariantCulture),
            //    $"{notification.RequestOrgIdentifier}发起了资产机构调换申请");
            return Task.CompletedTask;
        }
        /// <summary>
        /// 资产调换完成的通知
        /// </summary>
        /// <param name="notification"></param>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        public Task Handle(AssetExchangeHandledEvent notification, CancellationToken cancellationToken)
        {
            //await _hubContext.Clients.User(notification.RequestOrgIdentifier).Notify(notification.TargetOrgIdentifier,
            //    DateTime.Now.ToLocalTime().ToString(CultureInfo.InvariantCulture),
            //    $"将{notification.AssetName}调换至{notification.ExchangeOrgIdentifier}的申请完成");
            return Task.CompletedTask;
        }
        /// <summary>
        /// 资产交回删除的通知
        /// </summary>
        /// <param name="notification"></param>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        public Task Handle(AssetReturnRemovedEvent notification, CancellationToken cancellationToken)
        {
            //await _hubContext.Clients.User(notification.RequestOrgIdentifier).Notify(notification.TargetOrgIdentifier,
            //    DateTime.Now.ToLocalTime().ToString(CultureInfo.InvariantCulture),
            //    $"将{notification.AssetName}交回{notification.TargetOrgIdentifier}的申请已删除。");
            return Task.CompletedTask;
        }
        /// <summary>
        /// 资产调换撤销的通知
        /// </summary>
        /// <param name="notification"></param>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        public Task Handle(AssetExchangeRevokedEvent notification, CancellationToken cancellationToken)
        {
            // do nothing
            //await _hubContext.Clients.User(notification.RequestOrgIdentifier).Notify(notification.TargetOrgIdentifier,
            //    DateTime.Now.ToLocalTime().ToString(CultureInfo.InvariantCulture),
            //    $"将{notification.AssetName}调换至{notification.ExchangeOrgIdentifier}的申请没有通过审核");
            return Task.CompletedTask;
        }
        #endregion
    }
}