using Boc.Assets.Domain.Core.Bus;
using Boc.Assets.Domain.Core.Commands;
using Boc.Assets.Domain.Core.Notifications;
using Boc.Assets.Domain.Core.SharedKernel;
using MediatR;
using System.Threading.Tasks;

namespace Boc.Assets.Domain.CommandHandlers
{
    public class CommandHandler
    {
        protected readonly IUnitOfWork _unitOfWork;
        protected readonly IBus _bus;
        protected readonly DomainNotificationHandler _notifications;

        public CommandHandler(
            IUnitOfWork unitOfWork,
            IBus bus,
            INotificationHandler<DomainNotification> notifications)
        {
            _unitOfWork = unitOfWork;
            _bus = bus;
            _notifications = (DomainNotificationHandler)notifications;
        }
        protected async Task NotifyValidationErrors(Command command)
        {
            foreach (var error in command.ValidationResult.Errors)
            {
                await _bus.RaiseEventAsync(new DomainNotification(command.GetType().Name, error.ErrorMessage));
            }
        }
        public async Task<bool> CommitAsync()
        {
            //首先检查DomainNotification里面有没有FluentValidation检查出来的command错误
            if (_notifications.HasNotifications()) return false;
            //然后检查是否有需要提交的事物
            if (await _unitOfWork.SaveChangesAsync()) return true;
            //如果以上两项都不符合预期，那么就给DomainNotification里面添加一个错误。然后返回false
            await _bus.RaiseEventAsync(new DomainNotification("Commit", "保存你的数据期间存在一个问题"));
            return false;
        }
    }
}