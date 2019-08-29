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
        protected readonly IUnitOfWork UnitOfWork;
        protected readonly IBus Bus;
        protected readonly DomainNotificationHandler Notifications;

        public CommandHandler(
            IUnitOfWork unitOfWork,
            IBus bus,
            INotificationHandler<DomainNotification> notifications)
        {
            UnitOfWork = unitOfWork;
            Bus = bus;
            Notifications = (DomainNotificationHandler)notifications;
        }
        protected async Task NotifyValidationErrors(Command command)
        {
            foreach (var error in command.ValidationResult.Errors)
            {
                await Bus.RaiseEventAsync(new DomainNotification(command.GetType().Name, error.ErrorMessage));
            }
        }
        public async Task<bool> CommitAsync()
        {
            //首先检查DomainNotification里面有没有FluentValidation检查出来的command错误
            if (Notifications.HasNotifications()) return false;
            //然后检查是否有需要提交的事物
            if (await UnitOfWork.SaveChangesAsync()) return true;
            //如果以上两项都不符合预期，那么就给DomainNotification里面添加一个错误。然后返回false
            await Bus.RaiseEventAsync(new DomainNotification("提交", "保存你的数据期间存在一个问题，可能是由于数据没有变更的情况下提交导致的。"));
            return false;
        }
    }
}