using CookBook.RecipesWebapp.Server.Api.Users.Endpoints.LogIn.Contracts;
using FluentValidation;

namespace CookBook.RecipesWebapp.Server.Api.Users.Endpoints.LogIn.Validators;

internal sealed class LogInRequestDtoValidator :
    AbstractValidator<LoginRequestDto>
{
    public LogInRequestDtoValidator()
    {
        RuleFor(request => request.Email)
            .NotEmpty();

        RuleFor(request => request.Password)
            .NotEmpty();
    }
}
