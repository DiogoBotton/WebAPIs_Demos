using Domains.Enums;
using Domains.Models.Addresses;
using Domains.Models.ResetPasswordCodes;
using Domains.SeedWork;

namespace Domains.Models.Users;

public class User : AbstractDomain
{
    public string Name { get; set; }
    public string Email { get; set; }
    public string Cellphone { get; set; }

    public Profile Profile { get; set; }
    public bool Status { get; set; }

    public byte[] PasswordHash { get; set; }
    public byte[] PasswordSalt { get; set; }
    public Address Address { get; set; }
    public IList<ResetPasswordCode> ResetPasswordCodes { get; set; }

    public DateTime? FirstAccess { get; set; }
}
