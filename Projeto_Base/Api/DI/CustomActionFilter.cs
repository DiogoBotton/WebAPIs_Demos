using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.AspNetCore.Mvc;
using Services.DTOs;

namespace Api.DI;

/// <summary>
/// Custom Action Filter
/// </summary>
public class CustomActionFilter : IActionFilter
{
    /// <summary>
    /// On Action Executing
    /// </summary>
    public void OnActionExecuting(ActionExecutingContext context)
    {
    }

    /// <summary>
    /// On Action Executed
    /// </summary>
    public void OnActionExecuted(ActionExecutedContext context)
    {
        if (context.Result is ObjectResult objectResult)
        {
            // Verifica o tipo de resultado e ajusta o status code conforme necessário
            if (objectResult.Value is IBaseResponse baseResponse)
            {
                context.HttpContext.Response.StatusCode = (int)baseResponse.StatusCode;
            }
        }
    }
}
