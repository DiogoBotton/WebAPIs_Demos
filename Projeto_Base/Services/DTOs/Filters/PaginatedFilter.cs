namespace Services.DTOs.Filters;

public class PaginatedFilter : AbstractPageFilter
{
    public bool IsPaginated { get; set; } = true;
}
