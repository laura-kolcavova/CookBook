using FluentValidation;

namespace CookBook.Recipes.Api.Recipes.Endpoints.GetLatestRecipes.Validators;

internal sealed class GetLatestRecipesValidator :
    AbstractValidator<GetLatestRecipesParams>
{
    public GetLatestRecipesValidator()
    {
        RuleFor(request => request.Count)
             .GreaterThanOrEqualTo(0)
             .LessThanOrEqualTo(10);
    }
}
