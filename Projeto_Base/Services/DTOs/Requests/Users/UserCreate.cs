using Domains.Enums;
using Services.DTOs.Requests.Addresses;

namespace Services.DTOs.Requests.Users;

public class UserCreate
{
    public string Name { get; set; }
    public string Email { get; set; }
    public string Cellphone { get; set; }
    public Profile Profile { get; set; }

    public AddressCreate Address { get; set; }
}
