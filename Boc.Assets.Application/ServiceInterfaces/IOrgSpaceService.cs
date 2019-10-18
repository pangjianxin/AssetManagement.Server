using Boc.Assets.Application.Dto;
using Boc.Assets.Application.ViewModels.OrganizationSpace;
using Boc.Assets.Domain.Models.Organizations;
using System;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace Boc.Assets.Application.ServiceInterfaces
{
    public interface IOrgSpaceService : IApplicationService
    {
        Task CreateAsync(CreateSpace model);
        IQueryable<OrgSpaceDto> Get(Expression<Func<OrganizationSpace, bool>> predicate = null);
        Task ModifyAsync(ModifySpaceInfo model);
    }
}