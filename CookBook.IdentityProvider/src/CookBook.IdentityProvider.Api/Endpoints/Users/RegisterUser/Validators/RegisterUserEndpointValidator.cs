using FluentValidation;

namespace CookBook.IdentityProvider.Api.Endpoints.Users.RegisterUser.Validators;

internal sealed class RegisterUserEndpointValidator :
    AbstractValidator<RegisterUserEndpointParams>
{
    public RegisterUserEndpointValidator()
    {
        RuleFor(request => request.RegisterUserRequest)
           .NotNull()
           .SetValidator(new RegisterUserRequestDtoValidator());
    }
}
