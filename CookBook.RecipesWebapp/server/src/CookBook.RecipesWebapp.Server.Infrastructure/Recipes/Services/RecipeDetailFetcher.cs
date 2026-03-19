using CookBook.RecipesWebapp.Server.Domain.Recipes.Models;
using CookBook.RecipesWebapp.Server.Domain.Recipes.Services.Abastractions;
using CookBook.RecipesWebapp.Server.Infrastructure.Shared.Proxy.IdentityProviderApi.Clients;
using CookBook.RecipesWebapp.Server.Infrastructure.Shared.Proxy.IdentityProviderApi.Dto;
using CookBook.RecipesWebapp.Server.Infrastructure.Shared.Proxy.RecipesApi.Clients;
using CookBook.RecipesWebapp.Server.Infrastructure.Shared.Proxy.RecipesApi.Dto;
using CSharpFunctionalExtensions;
using Microsoft.Extensions.Logging;
using Refit;
using System.Net;

namespace CookBook.RecipesWebapp.Server.Infrastructure.Recipes.Services;

internal sealed class RecipeDetailFetcher(
    IRecipesApiClient recipesApiClient,
    IIdentityProviderApiClient identityProviderApiClient,
    ILogger<RecipeDetailFetcher> logger) :
    IRecipeDetailFetcher
{
    public async Task<Maybe<RecipeDetailModel>> FetchRecipeDetail(
        long recipeId,
        CancellationToken cancellationToken)
    {
        var recipeDetailResult = await GetRecipeDetailDto(
            recipeId,
            cancellationToken);

        if (recipeDetailResult.HasNoValue)
        {
            return Maybe.None;
        }

        var recipeDetailDto = recipeDetailResult.Value;

        var userProfileInfoResult = await GetUserProfileInfoDto(
            recipeDetailDto.UserName,
            cancellationToken);

        var recipeDetail = new RecipeDetailModel
        {
            RecipeId = recipeDetailDto.RecipeId,
            Title = recipeDetailDto.Title,
            Description = recipeDetailDto.Description,
            Servings = recipeDetailDto.Servings,
            CookTime = recipeDetailDto.CookTime,
            Notes = recipeDetailDto.Notes,
            CreatedAt = recipeDetailDto.CreatedAt,
            UserProfileInfo = userProfileInfoResult.HasValue
                ? new RecipeDetailModel.UserProfileInfoModel
                {
                    DisplayName = userProfileInfoResult.Value.DisplayName,
                    UserName = userProfileInfoResult.Value.UserName,
                }
                : new RecipeDetailModel.UserProfileInfoModel
                {
                    DisplayName = string.Empty,
                    UserName = string.Empty
                },
            Ingredients = recipeDetailDto
                .Ingredients
                .Select(
                    ingredient => new RecipeDetailModel.IngredientItemModel
                    {
                        LocalId = ingredient.LocalId,
                        Note = ingredient.Note,
                    })
                .ToList(),
            Instructions = recipeDetailDto
                .Instructions
                .Select(
                    instruction => new RecipeDetailModel.InstructionItemModel
                    {
                        LocalId = instruction.LocalId,
                        Note = instruction.Note,
                    })
                .ToList(),
            Tags = recipeDetailDto
                .Tags
                .ToList(),
        };

        return recipeDetail;
    }

    private async Task<Maybe<GetRecipeDetailResponseDto.RecipeDetailDto>> GetRecipeDetailDto(
        long recipeId,
        CancellationToken cancellationToken)
    {
        using var loggerScope = logger.BeginScope(new Dictionary<string, object>
        {
            ["RecipeId"] = recipeId,
        });

        try
        {
            var response = await recipesApiClient.GetRecipeDetail(
                recipeId,
                cancellationToken);

            if (response.StatusCode == HttpStatusCode.NoContent)
            {
                logger.LogWarning(
                    "Recipe detail not found in Recipes API");

                return Maybe.None;
            }

            if (!response.IsSuccessful)
            {
                throw response.Error;
            }

            return response.Content.RecipeDetail;
        }
        catch (OperationCanceledException)
        {
            throw;
        }
        catch (ApiException ex)
        {
            logger.LogError(
                ex,
                "Getting recipe detail failed in Recipes API with [{StatusCode}], [{Message}]",
                ex.StatusCode,
                ex.Message);

            throw;
        }
        catch (Exception ex)
        {
            logger.LogError(
                ex,
                "An unexpected error occurred while getting recipe detail in Recipes API");

            throw;
        }
    }

    private async Task<Maybe<GetUserProfileInfoResponseDto.UserProfileInfoDto>> GetUserProfileInfoDto(
        string userName,
        CancellationToken cancellationToken)
    {
        using var loggerScope = logger.BeginScope(new Dictionary<string, object>
        {
            ["UserName"] = userName,
        });

        try
        {
            var response = await identityProviderApiClient.GetUserProfileInfo(
                userName,
                cancellationToken);

            if (response.StatusCode == HttpStatusCode.NoContent)
            {
                logger.LogWarning(
                    "User profile info not found in Identity Provider API");

                return Maybe.None;
            }

            if (!response.IsSuccessful)
            {
                throw response.Error;
            }

            return response.Content.UserProfileInfo;
        }
        catch (OperationCanceledException)
        {
            throw;
        }
        catch (ApiException ex)
        {
            logger.LogError(
                ex,
                "Getting user profile info in Identity Provider API failed with [{StatusCode}], [{Message}]",
                ex.StatusCode,
                ex.Message);

            throw;
        }
        catch (Exception ex)
        {
            logger.LogError(
                ex,
                "An unexpected error occurred while getting user profile info in Identity Provider API");

            throw;
        }
    }
}
