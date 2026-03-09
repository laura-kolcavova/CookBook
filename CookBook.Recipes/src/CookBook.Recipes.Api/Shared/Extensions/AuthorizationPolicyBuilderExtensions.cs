using Microsoft.AspNetCore.Authorization;
using System.Security.Claims;

namespace CookBook.Recipes.Api.Shared.Extensions;

internal static class AuthorizationPolicyBuilderExtensions
{
    public static AuthorizationPolicyBuilder RequireScope(
        this AuthorizationPolicyBuilder builder,
        string scope)
    {
        return builder.RequireAssertion(
            context =>
            {
                var scopeValue = context.User.FindFirstValue("scope");

                if (string.IsNullOrEmpty(scopeValue))
                {
                    return false;
                }

                var scopes = scopeValue.Split(
                    ' ',
                    StringSplitOptions.RemoveEmptyEntries);

                return scopes.Contains(
                    scope,
                    StringComparer.Ordinal);
            });
    }
}
