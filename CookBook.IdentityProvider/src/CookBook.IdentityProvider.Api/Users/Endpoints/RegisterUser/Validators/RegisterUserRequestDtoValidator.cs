using CookBook.IdentityProvider.Api.Users.Endpoints.RegisterUser.Contracts;
using FluentValidation;

namespace CookBook.IdentityProvider.Api.Users.Endpoints.RegisterUser.Validators;

internal sealed class RegisterUserRequestDtoValidator :
    AbstractValidator<RegisterUserRequestDto>
{
    public RegisterUserRequestDtoValidator()
    {
        RuleFor(request => request.DisplayName)
            .NotEmpty()
            .MaximumLength(256);

        RuleFor(request => request.Email)
            .NotEmpty()
            .MaximumLength(256)
            .EmailAddress();

        RuleFor(request => request.Password)
            .NotEmpty()
            .MaximumLength(256);
    }
}
