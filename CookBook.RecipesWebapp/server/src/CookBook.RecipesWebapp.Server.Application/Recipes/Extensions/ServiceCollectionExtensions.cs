using CookBook.RecipesWebapp.Server.Application.Recipes.UseCases.GetRecipeDetail;
using Microsoft.Extensions.DependencyInjection;

namespace CookBook.RecipesWebapp.Server.Application.Recipes.Extensions;

internal static class ServiceCollectionExtensions
{
    public static IServiceCollection AddRecipes(
        this IServiceCollection services)
    {
        services
            .AddScoped<IGetRecipeDetailUseCase, GetRecipeDetailUseCase>();

        return services;
    }
}
