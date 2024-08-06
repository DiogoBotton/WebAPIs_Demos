using System.Net;

namespace Services.DTOs;

public class Response : IBaseResponse
{
    private static Response _success;
    public static Response Success => _success ??= new Response { StatusCode = HttpStatusCode.OK };
    public static Response SuccessWithStatus(HttpStatusCode statusCode = HttpStatusCode.OK) => new() { StatusCode = statusCode };

    public ErrorResponse Error { get; set; }
    public HttpStatusCode StatusCode { get; set; }

    public static Response Throw(ErrorResponse errorResponse, HttpStatusCode statusCode = HttpStatusCode.BadRequest) => new()
    {
        Error = errorResponse,
        StatusCode = statusCode
    };

    public static implicit operator Response(ErrorResponse errorResponse) => Throw(errorResponse);
}
