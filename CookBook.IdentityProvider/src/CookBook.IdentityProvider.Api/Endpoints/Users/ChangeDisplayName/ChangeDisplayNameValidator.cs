using FluentValidation;

namespace CookBook.IdentityProvider.Api.Endpoints.Users.ChangeDisplayName;

internal sealed class ChangeDisplayNameValidator :
    AbstractValidator<ChangeDisplayNameParams>
{
    public ChangeDisplayNameValidator()
    {
        RuleFor(r => r.ChangeDisplayNameRequest.DisplayName)
            .MaximumLength(256);
    }
}
