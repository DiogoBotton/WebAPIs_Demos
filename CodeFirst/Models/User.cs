namespace CodeFirst.Models;

public class User : BaseModel
{
    public string Name { get; set; }
    public string Email { get; set; }

    public Guid TypeUserId { get; set; }
    public TypeUser TypeUser { get; set; }

    public User(string name, string email, Guid typeUserId)
    {
        Name = name;
        Email = email;
        TypeUserId = typeUserId;
    }
}
