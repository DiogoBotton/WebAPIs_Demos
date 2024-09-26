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

    public async Task<ResponseOf<RegisterResult<Guid>>> Create(UserCreate request, CancellationToken cancellationToken)
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
            return new Error("Internal_Server_Error", "Houve algum erro na realização da operação.")
                .WithErrorStatusCode<RegisterResult<Guid>>(HttpStatusCode.InternalServerError);
        }
    }

    public async Task<Response> Delete(Guid Id, CancellationToken cancellationToken)
    {
        try
        {
            var user = await _userRepository.GetById(Id);

            if (user == null)
                return Response.ErrorHandle("User_Not_Found", "Usuário não encontrado.", HttpStatusCode.NotFound);

            user.DeletedAt = DateTime.Now;

            await _userRepository.UnitOfWork.SaveDbChanges(cancellationToken);

            return Response.Success();
        }
        catch (Exception ex)
        {
            return Response.ErrorHandle("Internal_Server_Error", $"{ex.StackTrace}", HttpStatusCode.InternalServerError);
        }
    }

    public async Task<ResponseOf<UserResult>> Detail(Guid Id, CancellationToken cancellationToken)
    {
        try
        {
            var user = await _userRepository.GetById(Id, x => x.Address);

            if (user == null)
                return ErrorFactory.NotFoundError("Usuário não encontrado.");

            return user.Adapt<UserResult>();
        }
        catch (Exception)
        {
            return new Error("Internal_Server_Error", "Houve algum erro na realização da operação.")
                .WithErrorStatusCode<UserResult>(HttpStatusCode.InternalServerError);
        }
    }

    public async Task<ResponseOf<PageResult<UserResult>>> ListAll(PaginatedFilter request, CancellationToken cancellationToken)
    {
        try
        {
            var users = _userRepository
                .List(x => x.Address);

            if (!string.IsNullOrEmpty(request.Search))
                users = users.Where(x => x.Name.Contains(request.Search) || x.Email.Contains(request.Search));

            if (request.Status.HasValue)
                users = users.Where(x => x.Status == request.Status);

            var result = await users
                .ProjectToType<UserResult>()
                .PaginateBy(request, x => x.Name)
                .ToListAsync(cancellationToken);

            return new PageResult<UserResult>(result);
        }
        catch (Exception)
        {
            return new Error("Internal_Server_Error", "Houve algum erro na realização da operação.")
                .WithErrorStatusCode<PageResult<UserResult>>(HttpStatusCode.InternalServerError);
        }
    }

    public async Task<Response> Update(UserUpdate request, CancellationToken cancellationToken)
    {
        try
        {
            var user = await _userRepository.GetById(request.Id, x => x.Address);

            if (user == null)
                return Response.ErrorHandle("User_Not_Found", "Usuário não encontrado.", HttpStatusCode.NotFound);

            if (request.Email != user.Email)
                if (await _userRepository.EmailExists(request.Email, cancellationToken))
                    return Response.ErrorHandle("Email_Exists", "O e-mail informado já está em uso.");

            request.Adapt(user);

            await _userRepository.UnitOfWork.SaveDbChanges(cancellationToken);

            return Response.Success();
        }
        catch (Exception)
        {
            return Response.ErrorHandle("Internal_Server_Error", "Houve algum erro na realização da operação.", HttpStatusCode.InternalServerError);
        }
    }
}
