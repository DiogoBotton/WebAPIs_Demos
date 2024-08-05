using Api.Validators.Unique;
using FluentValidation;
using Infrastructure.Contexts;
using Services.DTOs.Requests.Users;

namespace Api.Validators;

internal class UserUpdateValidator : AbstractValidator<UserUpdate>
{
    public UserUpdateValidator(ApiDbContext db)
    {
        RuleFor(x => x.Email)
            .NotEmpty()
            .EmailAddress();

        RuleFor(x => x.Name)
            .NotEmpty()
            .MaximumLength(100);

        RuleFor(x => x.Cellphone)
            .NotEmpty()
            .MaximumLength(20);
    }
}
