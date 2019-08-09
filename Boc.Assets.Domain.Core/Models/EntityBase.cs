using System;

namespace Boc.Assets.Domain.Core.Models
{
    public class EntityBase : EntityBase<Guid>, IEntity
    {

    }
    public class EntityBase<TKey> : IEntity<TKey>
    {
        public TKey Id { get; set; }

        public override bool Equals(object obj)
        {
            var compareTo = obj as EntityBase<TKey>;

            if (ReferenceEquals(this, compareTo)) return true;
            if (compareTo is null) return false;

            return Id.Equals(compareTo.Id);
        }

        public static bool operator ==(EntityBase<TKey> a, EntityBase<TKey> b)
        {
            if (a is null && b is null)
                return true;

            if (a is null || b is null)
                return false;

            return a.Equals(b);
        }

        public static bool operator !=(EntityBase<TKey> a, EntityBase<TKey> b)
        {
            return !(a == b);
        }

        public override int GetHashCode()
        {
            return (GetType().GetHashCode() * 907) + Id.GetHashCode();
        }

        public override string ToString()
        {
            return GetType().Name + " [Id=" + Id + "]";
        }
    }
}