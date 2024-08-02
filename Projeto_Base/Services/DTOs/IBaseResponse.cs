using System.Net;

namespace Services.DTOs;

public interface IBaseResponse
{
    public HttpStatusCode StatusCode { get; set; }
}
