using Microsoft.AspNetCore.Mvc;

namespace CookBook.Recipes.Api.Shared.Dto;

internal sealed class OffsetFilterRequestDto
{
    [FromQuery]
    public int? Offset { get; init; }

    [FromQuery]
    public int? Limit { get; init; }

    //public static ValueTask<OffsetFilterRequestDto?> BindAsync(HttpContext context, ParameterInfo parameter)
    //{
    //    const string offsetKey = "offset";
    //    const string limitKey = "limit";

    //    int? offset = null;
    //    int? limit = null;

    //    if (context.Request.Query.ContainsKey(offsetKey))
    //    {
    //        if (int.TryParse(context.Request.Query[offsetKey], out var offsetValue))
    //        {
    //            offset = offsetValue;
    //        }
    //        else
    //        {
    //            return ValueTask.FromResult<OffsetFilterRequestDto?>(null);
    //        }
    //    }

    //    if (context.Request.Query.ContainsKey(limitKey))
    //    {
    //        if (int.TryParse(context.Request.Query[limitKey], out var limitValue))
    //        {
    //            limit = limitValue;
    //        }
    //        else
    //        {
    //            return ValueTask.FromResult<OffsetFilterRequestDto?>(null);
    //        }
    //    }

    //    var result = new OffsetFilterRequestDto
    //    {
    //        Limit = limit,
    //        Offset = offset,
    //    };

    //    return ValueTask.FromResult<OffsetFilterRequestDto?>(result);
    //}
}
