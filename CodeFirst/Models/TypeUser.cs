namespace CodeFirst.Models;

public class TypeUser : BaseModel
{
    public string Name { get; set; }

    public ICollection<User> Users { get; set; } = new List<User>();
}
