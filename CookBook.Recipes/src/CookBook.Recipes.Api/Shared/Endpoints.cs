﻿using CookBook.Recipes.Api.Recipes;

namespace CookBook.Recipes.Api.Shared;

internal static class Endpoints
{
    public static RouteGroupBuilder UseEndpoints(this WebApplication app)
    {
        return app
            .MapGroup("/api")
            .AddRecipesEndpoints();
    }
}
