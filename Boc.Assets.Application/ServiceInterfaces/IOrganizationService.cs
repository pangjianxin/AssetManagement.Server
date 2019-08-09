using Boc.Assets.Application.Dto;
using Boc.Assets.Application.Pagination;
using Boc.Assets.Application.Sieve.Models;
using Boc.Assets.Application.ViewModels.Login;
using Boc.Assets.Application.ViewModels.Organization;
using Boc.Assets.Domain.Models.Organizations;
using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace Boc.Assets.Application.ServiceInterfaces
{
    public interface IOrganizationService : IApplicationService
    {
        Task ChangeOrgShortNameAsync(ChangeOrgShortName model);
        Task<OrgDto> GetByOrgIdentifierAsync(string orgIdentifier);
        Task<bool> CheckLoginCredentialAsync(Login model);
        Task<OrgDto> GetByIdAsync(Guid id);
        Task<PaginatedList<OrgDto>> PaginationAsync(SieveModel model, Expression<Func<Organization, bool>> predicate = null);
        Task ResetOrgPassword(ResetOrgPassword model);
        Task ChangeOrgPassword(ChangeOrgPassword model);
        Task<IEnumerable<OrgDto>> GetTwentyAsync(string searchInput, string org2);
    }
}