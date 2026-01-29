namespace CookBook.IdentityProvider.Api.Shared.Extensions;

internal static class RouteHandlerBuilderExtensions
{
    public static RouteHandlerBuilder WithValidation(
        this RouteHandlerBuilder builder)
    {
        return builder
            .AddFluentValidationAutoValidation()
            .ProducesValidationProblem();
    }
}
