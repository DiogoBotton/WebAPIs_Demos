using System.Net;

namespace Services.DTOs;

public class ResponseOf<T> : IResponse
{
    public Error Error { get; set; }
    public T Result { get; set; }
    public HttpStatusCode StatusCode { get; set; }

    public ResponseOf() { }

    public ResponseOf(T result, HttpStatusCode statusCode = HttpStatusCode.OK)
    {
        Result = result;
        StatusCode = statusCode;
    }

    public ResponseOf(Error error, HttpStatusCode statusCode = HttpStatusCode.BadRequest)
    {
        Error = error;
        StatusCode = error.GetStatusCode().HasValue ? error.GetStatusCode().Value : statusCode;
    }


    public static implicit operator ResponseOf<T>(T data) => new ResponseOf<T>(data);

    public static implicit operator ResponseOf<T>(Error errorResponse) => new ResponseOf<T>(errorResponse);
}