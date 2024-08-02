namespace Domains.SeedWork;

public interface IEntity
{
    DateTime CreatedAt { get; set; }
    DateTime ModifiedAt { get; set; }
}
