using Boc.Assets.Application.Dto;
using Boc.Assets.Application.Pagination;
using Boc.Assets.Application.Sieve.Models;
using Boc.Assets.Application.ViewModels.OrganizationSpace;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Boc.Assets.Application.ServiceInterfaces
{
    public interface IOrgSpaceService : IApplicationService
    {
        Task CreateAsync(CreateSpace model);
        Task<PaginatedList<OrgSpaceDto>> Pagination(SieveModel model);
        Task ModifyAsync(ModifySpaceInfo model);
        Task<List<OrgSpaceDto>> GetAllListAsync();
    }
}