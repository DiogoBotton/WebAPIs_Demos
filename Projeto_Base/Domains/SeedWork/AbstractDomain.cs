namespace Domains.SeedWork;

public abstract class AbstractDomain : IEntity, IDeletable
{
    public virtual Guid Id { get; protected set; }
    public virtual DateTime CreatedAt { get; set; }
    public virtual DateTime ModifiedAt { get; set; }
    public virtual DateTime? DeletedAt { get; set; }
}
