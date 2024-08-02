using Domains.Models.Users;
using Services.DTOs;
using Services.DTOs.Requests.Users;
using Services.DTOs.Results;
using Services.DTOs.Results.Users;

namespace Services.Services.UserServices.Interfaces;

public interface IUserService
{
    Task<BaseResponse<ListResult<UserResult>>> ListAll(CancellationToken cancellationToken = default);
    Task<UserResult> Detail(Guid Id, CancellationToken cancellationToken = default);
    Task<BaseResponse<RegisterResult<Guid>>> Create(UserCreate request, CancellationToken cancellationToken = default);
    Task<bool> Update(UserCreate request, CancellationToken cancellationToken = default);
    Task<bool> Delete(Guid Id, CancellationToken cancellationToken = default);
}
