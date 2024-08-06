using Domains.Models.Users;
using Services.DTOs;
using Services.DTOs.Filters;
using Services.DTOs.Requests.Users;
using Services.DTOs.Results;
using Services.DTOs.Results.Users;

namespace Services.Services.UserServices.Interfaces;

public interface IUserService
{
    Task<BaseResponse<PageResult<UserResult>>> ListAll(PaginatedFilter request, CancellationToken cancellationToken = default);
    Task<BaseResponse<UserResult>> Detail(Guid Id, CancellationToken cancellationToken = default);
    Task<BaseResponse<RegisterResult<Guid>>> Create(UserCreate request, CancellationToken cancellationToken = default);
    Task<Response> Update(UserUpdate request, CancellationToken cancellationToken = default);
    Task<Response> Delete(Guid Id, CancellationToken cancellationToken = default);
}
