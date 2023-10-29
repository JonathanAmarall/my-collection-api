namespace MyCollection.Core.Contracts;

public interface IAuditableEntity
{
    public DateTime CreatedAt { get;  }
    public DateTime? UpdateAt { get;  }
}
