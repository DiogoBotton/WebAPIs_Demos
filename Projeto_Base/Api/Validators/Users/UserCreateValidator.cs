using Api.Validators.Unique;
using FluentValidation;
using Infrastructure.Contexts;
using Services.DTOs.Requests.Users;

namespace Api.Validators.Users;

internal class UserCreateValidator : AbstractValidator<UserCreate>
{
    public UserCreateValidator(ApiDbContext db)
    {
        RuleFor(x => x.Email)
            .NotEmpty()
            .EmailAddress()
            .SetAsyncValidator(new UniqueUserValidator<UserCreate>(db));

        RuleFor(x => x.Name)
            .NotEmpty()
            .MaximumLength(100);

        RuleFor(x => x.Cellphone)
            .NotEmpty()
            .MaximumLength(20);

        RuleFor(x => x.Profile)
            .NotNull()
            .IsInEnum();
    }
}
