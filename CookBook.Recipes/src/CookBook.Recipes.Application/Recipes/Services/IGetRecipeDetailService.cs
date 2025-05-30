﻿using CookBook.Recipes.Domain.Recipes.ReadModels;
using CSharpFunctionalExtensions;

namespace CookBook.Recipes.Application.Recipes.Services;

public interface IGetRecipeDetailService
{
    public Task<Maybe<RecipeDetailReadModel>> GetRecipeDetail(
       long recipeId,
       CancellationToken cancellationToken);
}
