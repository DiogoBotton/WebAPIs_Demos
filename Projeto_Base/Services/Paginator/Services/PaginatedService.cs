using Domains.SeedWork;
using Infrastructure.Contexts;
using Microsoft.EntityFrameworkCore;
using Services.DTOs.Results;
using Services.Paginator.Services.Interfaces;

namespace Services.Paginator.Services;

public class PaginatedService<T> : IPaginatedService<T> where T : AbstractDomain
{
    private ApiDbContext _ctx { get; set; }

    public PaginatedService(ApiDbContext context)
    {
        _ctx = context;
    }

    public async Task<PaginationDataResult> GetPaginatedDataAsync(PaginationDataRequest request, CancellationToken cancellationToken = default)
    {
        var totalItems = await _ctx.Set<T>().OnlyActives().CountAsync(cancellationToken);
        var pageCount = (int)Math.Ceiling(totalItems / (double)request.PageSize);

        return new PaginationDataResult(pageCount, request.PageSize);
    }
}
