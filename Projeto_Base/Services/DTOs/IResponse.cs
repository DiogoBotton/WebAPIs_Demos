using System.Net;

namespace Services.DTOs;

public interface IResponse
{
    public HttpStatusCode StatusCode { get; set; }
}
