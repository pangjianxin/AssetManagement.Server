namespace Boc.Assets.Domain.Models.Organizations
{
    public interface IPasswordHasher
    {
        byte[] Hash(string password, byte[] salt);
    }
}