using CookBook.Recipes.Api.Shared.Dto;
using FluentValidation;

namespace CookBook.Recipes.Api.Shared.Validators;

internal sealed class OffsetFilterRequestValidator : AbstractValidator<OffsetFilterRequestDto>
{
    public OffsetFilterRequestValidator()
    {
        RuleFor(request => request.Offset)
            .GreaterThanOrEqualTo(0);

        RuleFor(request => request.Limit)
            .GreaterThanOrEqualTo(0);
    }
}
