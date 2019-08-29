using Boc.Assets.Domain.Commands.Employee;
using Boc.Assets.Domain.Core.Bus;
using Boc.Assets.Domain.Core.Notifications;
using Boc.Assets.Domain.Core.SharedKernel;
using Boc.Assets.Domain.Events;
using Boc.Assets.Domain.Repositories;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace Boc.Assets.Domain.CommandHandlers.Employee
{
    public class EmployeeCommandHandler : CommandHandler,
        IRequestHandler<AddEmployeeCommand, bool>
    {
        private readonly IEmployeeRepository _employeeRepository;

        public EmployeeCommandHandler(IUnitOfWork unitOfWork,
            IBus bus,
            INotificationHandler<DomainNotification> notifications,
            IEmployeeRepository employeeRepository) : base(unitOfWork, bus, notifications)
        {
            _employeeRepository = employeeRepository;
        }

        public async Task<bool> Handle(AddEmployeeCommand request, CancellationToken cancellationToken)
        {
            if (! request.IsValid())
            {
                await NotifyValidationErrors(request);
                return false;
            }

            var employeeExist = _employeeRepository.GetAll(it => it.Identifier == request.Identifier);
            if (await employeeExist.AnyAsync())
            {
                await _bus.RaiseEventAsync(new DomainNotification("400", "提交的员工号已存在，请查证"));
                return false;
            }
            var employee = new Models.Organizations.Employee()
            {
                Id = Guid.NewGuid(),
                Identifier = request.Identifier,
                Name = request.Name,
                Org2 = request.Org2,
                Telephone = request.Telephone,
                OfficePhone = request.OfficePhone
            };
            var result = await _employeeRepository.AddAsync(employee);
            if (await CommitAsync())
            {
                await _bus.RaiseEventAsync(new NonAuditEvent(request.Principal, NonAuditEventType.机构添加员工));
                return true;
            }
            return false;
        }
    }
}