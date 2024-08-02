using Domains.Models.Users;
using Domains.Models.Users.Interfaces;
using FluentValidation;
using Mapster;
using Microsoft.EntityFrameworkCore;
using Services.DTOs;
using Services.DTOs.Requests.Users;
using Services.DTOs.Results;
using Services.DTOs.Results.Users;
using Services.Extensions;
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
            await _userRepository.UnitOfWork.SaveDbChanges();

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

    public async Task<bool> Delete(Guid Id, CancellationToken cancellationToken)
    {
        throw new NotImplementedException();
    }

    public async Task<UserResult> Detail(Guid Id, CancellationToken cancellationToken)
    {
        throw new NotImplementedException();
    }

    public async Task<BaseResponse<ListResult<UserResult>>> ListAll(CancellationToken cancellationToken)
    {
        try
        {
            var users = await _userRepository
                .List(x => x.Address)
                .ProjectToType<UserResult>()
                .ToListAsync(cancellationToken);

            return new ListResult<UserResult>
            {
                Items = users
            };
        }
        catch (Exception)
        {
            return new ErrorResponse("Internal_Server_Error", "Houve algum erro na realização da operação.")
                .WithErrorStatusCode<ListResult<UserResult>>(HttpStatusCode.InternalServerError);
        }
    }

    public async Task<bool> Update(UserCreate model, CancellationToken cancellationToken)
    {
        throw new NotImplementedException();
    }
}
