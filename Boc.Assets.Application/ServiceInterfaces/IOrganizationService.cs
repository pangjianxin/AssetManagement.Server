using Boc.Assets.Application.Dto;
using Boc.Assets.Application.ViewModels.Login;
using Boc.Assets.Application.ViewModels.Organization;
using Boc.Assets.Domain.Models.Organizations;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace Boc.Assets.Application.ServiceInterfaces
{
    public interface IOrganizationService : IApplicationService
    {
        Task<string> ChangeOrgShortNameAsync(ChangeOrgShortName model);
        Task<string> LoginAsync(Login model);
        Task<bool> ResetOrgPassword(ResetOrgPassword model);
        Task<string> ChangeOrgPassword(ChangeOrgPassword model);
        Task<OrgDto> Get(Guid id);
        IQueryable<OrgDto> Get(Expression<Func<Organization, bool>> predicate = null);
        Task<IEnumerable<OrgDto>> Get(string searchInput, string org2);
        Task PushHash();
    }
}