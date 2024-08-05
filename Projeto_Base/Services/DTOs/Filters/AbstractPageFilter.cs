using Services.DTOs.Requests;

namespace Services.DTOs.Filters;

public abstract class AbstractPageFilter : PageRequest
{
    public virtual string Search { get; set; }
    public bool? Status { get; set; }
}
