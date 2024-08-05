using Domains.SeedWork;
using Services.DTOs.Results;

namespace Services.Paginator.Services.Interfaces;

public interface IPaginatedService<T> where T : AbstractDomain
{
    Task<PaginationDataResult> GetPaginatedDataAsync(PaginationDataRequest request, CancellationToken cancellationToken = default);
}
