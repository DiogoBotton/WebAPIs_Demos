using Domains.Enums;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;

namespace Domains.Security;

public class AuthorizeByClaimAttribute : TypeFilterAttribute
{
    public AuthorizeByClaimAttribute(params Profile[] controllerRoles) : base(typeof(AuthorizeByClaimFilter))
    {
        Arguments = new object[] { controllerRoles };
    }
}

public class AuthorizeByClaimFilter : IAuthorizationFilter
{
    private readonly Profile[] controllerRoles;

    public AuthorizeByClaimFilter(Profile[] controllerRoles)
    {
        this.controllerRoles = controllerRoles;
    }

    public void OnAuthorization(AuthorizationFilterContext context)
    {
        if (!context.HttpContext.User.Claims.Any())
        {
            context.Result = new ForbidResult();
            return;
        }

        var userRoles = context.HttpContext.User.Claims
                .FirstOrDefault(d => d.Type == "roles").Value
                .Split(",")
                .Select(d => Enum.Parse<Profile>(d));

        if (!controllerRoles.Any(cr => userRoles.Select(ur => ur).Any(r => r == cr)))
            context.Result = new ForbidResult();
    }
}