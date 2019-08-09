using System;

namespace Boc.Assets.Domain.Core.Models
{
    public interface IEntity<TKey>
    {
        TKey Id { get; set; }
    }
    public interface IEntity : IEntity<Guid> { }
}