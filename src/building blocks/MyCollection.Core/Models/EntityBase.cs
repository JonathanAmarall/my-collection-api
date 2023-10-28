namespace MyCollection.Core.Models
{
    public abstract class EntityBase 
    {
        protected EntityBase()
        {
            Id = Guid.NewGuid();
            CreatedAt = DateTime.Now;
        }

        protected EntityBase(Guid id)
        {
            if (id == Guid.Empty)
            {
                throw new ArgumentException("The identifier is required.", nameof(id));
            }

            Id = id;
            CreatedAt = DateTime.Now;
        }

        public Guid Id { get; private set; }
        public DateTime CreatedAt { get; private set; }
        public DateTime? UpdateAt { get; protected set; }

        public override bool Equals(object  other)
        {
            var compareTo = other as EntityBase;

            if (ReferenceEquals(this, compareTo)) return true;
            if (ReferenceEquals(null, compareTo)) return false;

            return Id.Equals(compareTo.Id);
        }

        public static bool operator ==(EntityBase a, EntityBase b)
        {
            if (ReferenceEquals(a, null) && ReferenceEquals(b, null))
                return true;
            if (ReferenceEquals(a, null) || ReferenceEquals(b, null))
                return false;

            return a.Equals(b);
        }

        public static bool operator !=(EntityBase a, EntityBase b)
        {
            return !(a == b);
        }

        public override int GetHashCode()
        {
            return (GetType().GetHashCode() * 907) + Id.GetHashCode();
        }

        public override string ToString()
        {
            return $"{GetType().Name} [Id={Id}]";
        }
    }
}
