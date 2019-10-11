using Boc.Assets.Domain.Models.Organizations;
using System.Threading.Tasks;

namespace Boc.Assets.Domain.Authentication
{
    public interface IJwtFactory
    {
        Task<string> CreateTokenAsync(Organization org);
    }
}