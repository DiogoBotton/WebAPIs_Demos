using Services.DTOs;
using System.Net;

namespace Services.Extensions;

public static class ResponseExtensions
{
    public static BaseResponse<T> WithSuccessStatusCode<T>(this T result, HttpStatusCode statusCode = HttpStatusCode.OK)
    {
        return new BaseResponse<T>(result, statusCode);
    }

    public static BaseResponse<T> WithErrorStatusCode<T>(this ErrorResponse errorResponse, HttpStatusCode statusCode)
    {
        return new BaseResponse<T>(errorResponse, statusCode);
    }
}
