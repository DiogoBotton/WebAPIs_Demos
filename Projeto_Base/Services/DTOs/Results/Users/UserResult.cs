using Domains.Enums;
using Services.DTOs.Results.Addresses;

namespace Services.DTOs.Results.Users;

public class UserResult
{
    public Guid Id { get; set; }
    public string Name { get; set; }
    public string Email { get; set; }
    public string Cellphone { get; set; }
    public Profile Profile { get; set; }
    public bool Status { get; set; }

    public AddressResult Address { get; set; }
}
