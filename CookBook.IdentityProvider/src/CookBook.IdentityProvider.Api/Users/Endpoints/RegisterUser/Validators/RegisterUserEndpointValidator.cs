using FluentValidation;

namespace CookBook.IdentityProvider.Api.Users.Endpoints.RegisterUser.Validators;

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
