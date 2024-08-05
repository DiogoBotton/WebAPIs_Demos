using Domains.Enums;
using Microsoft.AspNetCore.Http;
using System.Security.Claims;

namespace Domains.Security;

public class Actor
{
    public Guid Subject { get; set; }

    public IEnumerable<Profile> Roles { get; set; } = new List<Profile>();

    public IEnumerable<Claim> Claims { get; set; }

    public ClaimsPrincipal ClaimsPrincipal { get; set; }

    public Actor(IHttpContextAccessor accessor)
    {
        Console.WriteLine();

        ClaimsPrincipal = accessor.HttpContext.User;
        Claims = accessor.HttpContext.User.Claims;

        if (Guid.TryParse(Claims.FirstOrDefault(d => d.Type == "sub")?.Value, out var subject))
            Subject = subject;

        if (Claims.Where(d => d.Type == "roles").Any())
            Roles = Claims.FirstOrDefault(d => d.Type == "roles").Value.Split(",").Select(d => Enum.Parse<Profile>(d));
    }
}
