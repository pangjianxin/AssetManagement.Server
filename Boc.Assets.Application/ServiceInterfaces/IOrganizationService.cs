using Boc.Assets.Application.Dto;
using Boc.Assets.Application.Pagination;
using Boc.Assets.Application.ViewModels.Login;
using Boc.Assets.Application.ViewModels.Organization;
using Boc.Assets.Domain.Models.Organizations;
using Sieve.Models;
using System;
using System.Collections.Generic;
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
        Task<OrgDto> GetByIdAsync(Guid id);
        Task<PaginatedList<OrgDto>> PaginationAsync(SieveModel model, Expression<Func<Organization, bool>> predicate = null);
        Task<IEnumerable<OrgDto>> GetTwentyAsync(string searchInput, string org2);
        Task PushHash();
    }
}