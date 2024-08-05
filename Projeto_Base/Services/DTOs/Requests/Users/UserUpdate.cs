using Domains.Enums;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using Services.DTOs.Requests.Addresses;
using System.Text.Json.Serialization;

namespace Services.DTOs.Requests.Users;

public class UserUpdate
{
    [BindNever]
    [JsonIgnore]
    public Guid Id { get; set; }
    public string Name { get; set; }
    public string Email { get; set; }
    public string Cellphone { get; set; }

    public AddressCreate Address { get; set; }
}
