using Domains.Enums;

namespace Services.DTOs.Results.Users;

public class UserSimpleResult
{
    public Guid Id { get; set; }
    public string Name { get; set; }
    public string Email { get; set; }
    public string Cellphone { get; set; }

    public Profile Profile { get; set; }
    public bool Status { get; set; }
}
