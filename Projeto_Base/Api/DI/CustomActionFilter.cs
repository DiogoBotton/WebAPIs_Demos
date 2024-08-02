using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.AspNetCore.Mvc;
using Services.DTOs;

namespace Api.DI;

public class CustomActionFilter : IActionFilter
{
    public void OnActionExecuting(ActionExecutingContext context)
    {
    }

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
