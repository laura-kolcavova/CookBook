using CookBook.Recipes.Application.ValidationRules.Recipes;
using FluentValidation;

namespace CookBook.Recipes.Api.Features.Recipes.SaveRecipe;

internal class SaveRecipeRequestDtoValidator : AbstractValidator<SaveRecipeRequestDto>
{
    public SaveRecipeRequestDtoValidator()
    {
        RuleFor(request => request.RecipeId)
            .GreaterThanOrEqualTo(0);

        RuleFor(request => request.UserId)
           .GreaterThanOrEqualTo(0);

        RuleFor(request => request.Title)
            .NotNull()
            .RecipeTitle();

        RuleFor(request => request.Description)
            .RecipeDescription();

        RuleFor(request => request.Servings)
            .RecipeServings();

        RuleFor(request => request.PreparationTime)
            .RecipePreparationTime();

        RuleFor(request => request.ServingsTime)
            .RecipeServingsTime();

        RuleFor(request => request.Notes)
            .RecipeNotes();

        RuleForEach(request => request.Ingredients)
            .ChildRules(ingredient =>
            {
                ingredient.RuleFor(x => x.Id)
                    .GreaterThanOrEqualTo(0);

                ingredient.RuleFor(x => x.Note)
                    .NotNull()
                    .RecipeIngredientNote();
            });

        RuleForEach(request => request.Instructions)
           .ChildRules(instruction =>
           {
               instruction.RuleFor(x => x.Id)
                   .GreaterThanOrEqualTo(0);

               instruction.RuleFor(x => x.Note)
                   .NotNull()
                   .RecipeInstructionNote();
           });
    }
}
