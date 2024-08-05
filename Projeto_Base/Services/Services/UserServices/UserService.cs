using Domains.Models.Users;
using Domains.Models.Users.Interfaces;
using FluentValidation;
using Mapster;
using Microsoft.EntityFrameworkCore;
using Services.DTOs;
using Services.DTOs.Filters;
using Services.DTOs.Requests.Users;
using Services.DTOs.Results;
using Services.DTOs.Results.Users;
using Services.Extensions;
using Services.Paginator.Extensions;
using Services.Services.UserServices.Interfaces;
using System.Net;

namespace Services.Services.UserServices;

public class UserService : IUserService
{
    private readonly IUserRepository _userRepository;
    private readonly IValidator<UserCreate> _userCreateValidator;
    public UserService(IUserRepository repository, IValidator<UserCreate> userCreateValidator)
    {
        _userRepository = repository;
        _userCreateValidator = userCreateValidator;
    }

    public async Task<BaseResponse<RegisterResult<Guid>>> Create(UserCreate request, CancellationToken cancellationToken)
    {
        try
        {
            var validationResult = await _userCreateValidator.ValidateAsync(request, cancellationToken);
            if (!validationResult.IsValid)
                return validationResult.ErrorResponse();

            var user = request.Adapt<User>();

            await _userRepository.CreateAsync(user, cancellationToken);
            await _userRepository.UnitOfWork.SaveDbChanges(cancellationToken);

            return new RegisterResult<Guid>
            {
                Id = user.Id
            };
        }
        catch (Exception)
        {
            return new ErrorResponse("Internal_Server_Error", "Houve algum erro na realização da operação.")
                .WithErrorStatusCode<RegisterResult<Guid>>(HttpStatusCode.InternalServerError);
        }
    }

    public async Task<BaseResponse<bool>> Delete(Guid Id, CancellationToken cancellationToken)
    {
        try
        {
            var user = await _userRepository.GetById(Id);

            if (user == null)
                return new ErrorResponse("User_Not_Found", "Usuário não encontrado.")
                    .WithErrorStatusCode<bool>(HttpStatusCode.NotFound);

            user.DeletedAt = DateTime.Now;

            await _userRepository.UnitOfWork.SaveDbChanges(cancellationToken);

            return true;
        }
        catch (Exception)
        {
            return new ErrorResponse("Internal_Server_Error", "Houve algum erro na realização da operação.")
                .WithErrorStatusCode<bool>(HttpStatusCode.InternalServerError);
        }
    }

    public async Task<BaseResponse<UserResult>> Detail(Guid Id, CancellationToken cancellationToken)
    {
        try
        {
            var user = await _userRepository.GetById(Id, x => x.Address);

            if (user == null)
                return new ErrorResponse("User_Not_Found", "Usuário não encontrado.")
                    .WithErrorStatusCode<UserResult>(HttpStatusCode.NotFound);

            return user.Adapt<UserResult>();
        }
        catch (Exception)
        {
            return new ErrorResponse("Internal_Server_Error", "Houve algum erro na realização da operação.")
                .WithErrorStatusCode<UserResult>(HttpStatusCode.InternalServerError);
        }
    }

    public async Task<BaseResponse<PageResult<UserResult>>> ListAll(PaginatedFilter filter, CancellationToken cancellationToken)
    {
        try
        {
            var users = _userRepository
                .List(x => x.Address);

            if (!string.IsNullOrEmpty(filter.Search))
                users = users.Where(x => x.Name.Contains(filter.Search) || x.Email.Contains(filter.Search));

            if (filter.Status.HasValue)
                users = users.Where(x => x.Status == filter.Status);

            var result = await users
                .ProjectToType<UserResult>()
                .PaginateBy(filter, x => x.Name)
                .ToListAsync(cancellationToken);

            return new PageResult<UserResult>(result);
        }
        catch (Exception)
        {
            return new ErrorResponse("Internal_Server_Error", "Houve algum erro na realização da operação.")
                .WithErrorStatusCode<PageResult<UserResult>>(HttpStatusCode.InternalServerError);
        }
    }

    public async Task<BaseResponse<bool>> Update(UserUpdate request, CancellationToken cancellationToken)
    {
        try
        {
            var user = await _userRepository.GetById(request.Id, x => x.Address);

            if (user == null)
                return new ErrorResponse("User_Not_Found", "Usuário não encontrado.")
                    .WithErrorStatusCode<bool>(HttpStatusCode.NotFound);

            if (request.Email != user.Email)
                if (await _userRepository.EmailExists(request.Email, cancellationToken))
                    return new ErrorResponse("Email_Already_Exists", "Este email já existe na plataforma.");

            request.Adapt(user);

            await _userRepository.UnitOfWork.SaveDbChanges(cancellationToken);

            return true;
        }
        catch (Exception)
        {
            return new ErrorResponse("Internal_Server_Error", "Houve algum erro na realização da operação.")
                .WithErrorStatusCode<bool>(HttpStatusCode.InternalServerError);
        }
    }
}
