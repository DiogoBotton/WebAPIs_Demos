using Microsoft.AspNetCore.Mvc;
using Services.DTOs;
using Services.DTOs.Requests.Users;
using Services.DTOs.Results;
using Services.DTOs.Results.Users;
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

    /// <summary>
    /// Users Controller Constructor
    /// </summary>
    public UsersController(IUserService userService)
    {
        _userService = userService;
    }

    /// <summary>
    /// List all users
    /// </summary>
    [HttpGet]
    public async Task<BaseResponse<ListResult<UserResult>>> Get(CancellationToken cancellationToken)
        => await _userService.ListAll(cancellationToken);

    /// <summary>
    /// Add a new user
    /// </summary>
    [HttpPost]
    public async Task<BaseResponse<RegisterResult<Guid>>> Post([FromBody] UserCreate request, CancellationToken cancellationToken)
        => await _userService.Create(request, cancellationToken);
}
