using Boc.Assets.Domain.Core.Repositories;
using Boc.Assets.Domain.Models.Organizations;
using System.Threading.Tasks;

namespace Boc.Assets.Domain.Repositories
{
    public interface IOrganizationRepository : IRepository<Organization>
    {
        Task<Organization> ChangeOrgShortNameAsync(string orgIdentifier, string orgShortName);
        Task<Organization> GetByOrgIdentifierAsync(string orgIdentifier);
        Task<Organization> ResetOrgPassword(string orgIdentifier);
        Task<Organization> ChangeOrgPassword(string orgIdentifier, string oldPassword, string newPassword);
    }
}