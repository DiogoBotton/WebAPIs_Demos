using Domains.Models.Users;
using Microsoft.AspNetCore.Mvc;
using Services.DTOs;
using Services.DTOs.Filters;
using Services.DTOs.Requests.Users;
using Services.DTOs.Results;
using Services.DTOs.Results.Users;
using Services.Paginator.Services.Interfaces;
using Services.Services.UserServices.Interfaces;

namespace Api.Controllers;

/// <summary>
/// Users Controller
/// </summary>
[Route("[controller]")]
[ApiController]
public class UsersController : ControllerBase
{
    private readonly IUserService _userService;
    private readonly IPaginatedService<User> _paginatedService;

    /// <summary>
    /// Users Controller Constructor
    /// </summary>
    public UsersController(IUserService userService, IPaginatedService<User> paginatedService)
    {
        _userService = userService;
        _paginatedService = paginatedService;
    }

    /// <summary>
    /// Get pagination metadata
    /// </summary>
    [HttpGet("pagination")]
    public async Task<ResponseOf<PaginationDataResult>> GetPaginationData([FromQuery] PaginationDataRequest request, CancellationToken cancellationToken)
        => await _paginatedService.GetPaginatedDataAsync(request, cancellationToken);

    /// <summary>
    /// List all users
    /// </summary>
    [HttpGet]
    public async Task<ResponseOf<PageResult<UserResult>>> Get([FromQuery] PaginatedFilter request, CancellationToken cancellationToken)
        => await _userService.ListAll(request, cancellationToken);

    /// <summary>
    /// Detail a User by Id
    /// </summary>
    [HttpGet("{UserId}")]
    public async Task<ResponseOf<UserResult>> GetById([FromRoute] Guid UserId, CancellationToken cancellationToken)
        => await _userService.Detail(UserId, cancellationToken);

    /// <summary>
    /// Add a new user
    /// </summary>
    [HttpPost]
    public async Task<ResponseOf<RegisterResult<Guid>>> Add([FromBody] UserCreate request, CancellationToken cancellationToken)
        => await _userService.Create(request, cancellationToken);

    /// <summary>
    /// Update a user
    /// </summary>
    [HttpPut("{UserId}")]
    public async Task<Response> Update([FromBody] UserUpdate request, [FromRoute] Guid UserId, CancellationToken cancellationToken)
    {
        request.Id = UserId;
        return await _userService.Update(request, cancellationToken);
    }

    /// <summary>
    /// Delete a user
    /// </summary>
    [HttpDelete("{UserId}")]
    public async Task<Response> Delete([FromRoute] Guid UserId, CancellationToken cancellationToken)
        => await _userService.Delete(UserId, cancellationToken);
}
