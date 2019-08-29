using Boc.Assets.Domain.Commands.Maintainers;
using Boc.Assets.Domain.Core.Bus;
using Boc.Assets.Domain.Core.Notifications;
using Boc.Assets.Domain.Core.SharedKernel;
using Boc.Assets.Domain.Repositories;
using MediatR;
using System.Threading;
using System.Threading.Tasks;

namespace Boc.Assets.Domain.CommandHandlers.Maintainer
{
    public class MaintainerCommandHandler : CommandHandler,
        IRequestHandler<AddMaintainerCommand, bool>,
        IRequestHandler<DeleteMaintainerCommand, bool>
    {
        private readonly IMaintainerRepository _maintainerRepository;

        public MaintainerCommandHandler(
            IUnitOfWork unitOfWork,
            IBus bus,
            INotificationHandler<DomainNotification> notifications,
            IMaintainerRepository maintainerRepository
         ) : base(unitOfWork, bus, notifications)
        {
            _maintainerRepository = maintainerRepository;
        }
        public async Task<bool> Handle(AddMaintainerCommand request, CancellationToken cancellationToken)
        {
            if (!request.IsValid())
            {
                await NotifyValidationErrors(request);
                return false;
            }
            var maintainer = new Models.Assets.Maintainer()
            {
                AssetCategoryId = request.AssetCategoryId,
                OrganizationId = request.OrganizationId,
                Org2 = request.Org2,
                CompanyName = request.CompanyName,
                MaintainerName = request.MaintainerName,
                Telephone = request.Telephone,
                OfficePhone = request.OfficePhone,
            };
            await _maintainerRepository.AddAsync(maintainer);
            if (!await CommitAsync())
            {
                return false;
            }
            return true;
        }

        public async Task<bool> Handle(DeleteMaintainerCommand request, CancellationToken cancellationToken)
        {
            var entity = await _maintainerRepository.GetByIdAsync(request.MaintainerId);
            if (entity == null)
            {
                await Bus.RaiseEventAsync(new DomainNotification("参数错误", "传入的主键未能找到相应的服务商实体"));
                return false;
            }
            _maintainerRepository.Remove(entity);
            if (!await CommitAsync())
            {
                return false;
            }
            return true;
        }


    }
}