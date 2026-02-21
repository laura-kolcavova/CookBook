using FluentValidation;

namespace CookBook.RecipesWebapp.Server.Api.Users.Endpoints.LogIn.Validators;

internal sealed class LogInEndpointValidator :
    AbstractValidator<LogInEndpointParams>
{
    public LogInEndpointValidator()
    {
        RuleFor(request => request.LogInRequest)
           .NotNull()
           .SetValidator(new LogInRequestDtoValidator());
    }
}
