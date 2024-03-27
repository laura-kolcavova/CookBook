using FluentValidation;

namespace CookBook.Recipes.Api.Categories.Features.GetCategoryDetail;

internal sealed class GetCategoryDetailRequestValidator : AbstractValidator<GetCategoryDetailRequestDto>
{
    public GetCategoryDetailRequestValidator()
    {
        RuleFor(request => request.CategoryId)
            .NotNull()
            .GreaterThan(0);
    }
}
