namespace MyCollection.Core.Models
{
    public abstract class EntityBase
    {
        public EntityBase()
        {
            CreatedAt = DateTime.Now;
        }

        public Guid Id { get; private set; }
        public DateTime CreatedAt { get; private set; }
        public DateTime? UpdateAt { get; protected set; }
    }
}
