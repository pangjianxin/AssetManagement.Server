using Boc.Assets.Domain.Core.Repositories;
using Boc.Assets.Domain.Models.Organizations;
using System.Threading.Tasks;

namespace Boc.Assets.Domain.Repositories
{
    public interface IOrganizationRepository : IRepository<Organization>
    {
        Task<Organization> GetByOrgIdentifierAsync(string orgIdentifier);
    }
}