namespace Boc.Assets.Application.Sieve.Models
{
    public interface ISortTerm
    {
        string Sort { set; }
        bool Descending { get; }
        string Name { get; }
    }
}