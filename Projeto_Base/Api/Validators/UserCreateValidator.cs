using FluentValidation;
using Services.DTOs.Requests.Users;

namespace Api.Validators;

internal class UserCreateValidator : AbstractValidator<UserCreate>
{
    public UserCreateValidator()
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

        RuleFor(x => x.Profile)
            .NotNull()
            .IsInEnum();
    }
}
