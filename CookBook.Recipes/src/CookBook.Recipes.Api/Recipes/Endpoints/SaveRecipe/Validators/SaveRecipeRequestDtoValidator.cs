using CookBook.Recipes.Api.Recipes.Endpoints.SaveRecipe.Contracts;
using CookBook.Recipes.Application.Recipes.ValidationRules;
using FluentValidation;

namespace CookBook.Recipes.Api.Recipes.Endpoints.SaveRecipe.Validators;

internal sealed class SaveRecipeRequestDtoValidator :
    AbstractValidator<SaveRecipeRequestDto>
{
    public SaveRecipeRequestDtoValidator()
    {
        RuleFor(request => request.RecipeId)
            .GreaterThan(0);

        RuleFor(request => request.UserId)
            .NotNull()
            .GreaterThan(0);

        RuleFor(request => request.Title)
            .NotNull()
            .RecipeTitle();

        RuleFor(request => request.Description)
            .RecipeDescription();

        RuleFor(request => request.Servings)
            .RecipeServings();

        RuleFor(request => request.PreparationTime)
            .RecipePreparationTime();

        RuleFor(request => request.CookTime)
            .RecipeCookTime();

        RuleFor(request => request.Notes)
            .RecipeNotes();

        RuleForEach(request => request.Ingredients)
            .ChildRules(ingredient =>
            {
                ingredient.RuleFor(x => x.LocalId)
                    .GreaterThan(0);

                ingredient.RuleFor(x => x.Note)
                    .NotNull()
                    .RecipeIngredientNote();
            });

        RuleForEach(request => request.Instructions)
           .ChildRules(instruction =>
           {
               instruction.RuleFor(x => x.LocalId)
                   .GreaterThan(0);

               instruction.RuleFor(x => x.Note)
                   .NotNull()
                   .RecipeInstructionNote();
           });
    }
}
