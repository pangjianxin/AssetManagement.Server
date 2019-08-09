using System.Threading.Tasks;

namespace Boc.Assets.Web.Auth.Authentication
{
    public interface IJwtFactory
    {
        Task<string> CreateTokenAsync(string orgIdentifier);
    }
}