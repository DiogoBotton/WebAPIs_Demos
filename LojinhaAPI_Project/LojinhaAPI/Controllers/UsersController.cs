using LojinhaAPI.Domains;
using LojinhaAPI.Infraestructure;
using LojinhaAPI.Infraestructure.Repositories;
using LojinhaAPI.Infraestructure.Repositories.Interfaces;
using LojinhaAPI.Requests;
using LojinhaAPI.Services.Interfaces;
using LojinhaAPI.ViewModels;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Net;

namespace LojinhaAPI.Controllers;

/// <summary>
/// Users Controllers
/// </summary>
[Route("api/[controller]")]
[ApiController]
public class UsersController : ControllerBase
{
    private readonly IUserRepository userRepository;
    private readonly ITypeUserRepository typeUserRepository;
    private readonly IUserServices userServices;

    /// <summary>
    /// Constructor
    /// </summary>
    public UsersController(IUserRepository userRepository, ITypeUserRepository typeUserRepository, IUserServices userServices)
    {
        this.userRepository = userRepository;
        this.typeUserRepository = typeUserRepository;
        this.userServices = userServices;
    }

    /// <summary>
    /// Get User by Id
    /// </summary>
    /// <param name="id">User Id</param>
    /// <param name="cancellationToken">Cancellation Token</param>
    /// <returns>Return an user by Id</returns>
    [HttpGet("{id}")]
    public async Task<IActionResult> GetById([FromRoute] long id, CancellationToken cancellationToken)
    {
        var result = await userServices.GetById(id, cancellationToken);
        
        if(result.Error != null)
            return StatusCode((int)result.Error.StatusCode, result.Error.Message);

        return Ok(result.ResultObject);
    }

    /// <summary>
    /// Get all Users
    /// </summary>
    [HttpGet]
    public async Task<IActionResult> GetAllUsers(CancellationToken cancellationToken)
    {
        List<User> users = await userRepository
            .ListAllAsync(cancellationToken);

        // Criando uma lista de UserViewModel
        List<UserViewModel> usersViewModels = new List<UserViewModel>();

        // Forma "na mão"
        foreach (var user in users)
        {
            TypeUserViewModel typeUserViewModel = new(user.TypeUserId, user.TypeUser.Name);

            UserViewModel userViewModel = new(user.Id, user.Name, user.Email, typeUserViewModel);

            usersViewModels.Add(userViewModel);
        }

        // Forma com LINQ (mais enxuta)
        //usersViewModels = users.Select(x => new UserViewModel(x.Id,x.Name,x.Email,
        //                            new TypeUserViewModel(x.TypeUserId, x.TypeUser.Name)))
        //                        .ToList();

        // Retornando a lista de usuários
        return Ok(usersViewModels); // 200 OK
    }

    /// <summary>
    /// Create an User
    /// </summary>
    /// <returns>Return id of User created</returns>
    [HttpPost]
    public async Task<IActionResult> Create([FromBody] UserRequest user, CancellationToken cancellationToken)
    {
        // Validação
        if (await userRepository.EmailExistsAsync(user.Email, cancellationToken))
            return BadRequest($"Usuário com email {user.Email} já existe no sistema.");

        if (!await typeUserRepository.TypeUserExistsAsync(user.TypeUserId, cancellationToken))
            return BadRequest("Tipo de usuário inválido.");

        User newUser = new User(user.Name, user.Email, user.TypeUserId);

        User userCreated = await userRepository.CreateAsync(newUser, cancellationToken);

        return Ok(new IdViewModel(userCreated.Id));
    }

    /// <summary>
    /// Update an User
    /// </summary>
    [HttpPut("{Id}")]
    public async Task<IActionResult> UpdateUser([FromBody] UserRequest userRequest, long Id, CancellationToken cancellationToken)
    {
        User? user = await userRepository.GetByIdAsync(Id, cancellationToken);

        #region Validações

        if (user == null)
            return NotFound("Usuário não encontrado");

        if (user.Email != userRequest.Email)
            if (await userRepository.EmailExistsAsync(userRequest.Email, cancellationToken))
                return BadRequest($"Usuário com email {userRequest.Email} já existe no sistema.");

        if (!await typeUserRepository.TypeUserExistsAsync(userRequest.TypeUserId, cancellationToken))
            return BadRequest("Tipo de usuário inválido.");
        #endregion

        user.Update(userRequest.Name, userRequest.Email, userRequest.TypeUserId);

        await userRepository.UpdateAsync(user, cancellationToken);

        return Ok("Usuário atualizado com sucesso");
    }

    /// <summary>
    /// Delete an User
    /// </summary>
    [HttpDelete("{Id}")]
    public async Task<IActionResult> DeleteUser(long Id, CancellationToken cancellationToken)
    {
        User? user = await userRepository.GetByIdAsync(Id, cancellationToken);

        if (user == null)
            return NotFound("Usuário não encontrado");

        await userRepository.DeleteAsync(user, cancellationToken);

        return Ok("Usuário deletado");
    }
}
