using Services.DTOs.Requests;

namespace Services.DTOs.Filters;

public abstract class AbstractPageFilter : PageRequest
{
    public override int MaxPageSize { get; set; } = 100;
    public virtual string Search { get; set; }
    public bool? Status { get; set; }
}
