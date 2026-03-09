//using CookBook.RecipesWebapp.Server.Api.Shared.Extensions;
//using Microsoft.AspNetCore.Authentication;
//using OpenIddict.Client.AspNetCore;

//namespace CookBook.RecipesWebapp.Server.Api.Users.Endpoints.LogIn;

//public sealed class LogInCallbackEndpointModule :
//    UsersModule
//{
//    public override void AddRoutes(IEndpointRouteBuilder app)
//    {
//        app
//            .MapGet("/callback/login/{provider}", Handle)
//            .WithName("LogIn Callback")
//            .WithSummary("Signs in an user")
//            .WithDescription("")
//            .Produces(StatusCodes.Status307TemporaryRedirect)
//            .ProducesValidationProblem()
//            .ProducesProblem(StatusCodes.Status401Unauthorized)
//            .ProducesProblem(StatusCodes.Status403Forbidden)
//            .ProducesProblem(StatusCodes.Status500InternalServerError)
//            .ValidateRequest()
//            .AllowAnonymous();
//    }

//    private static IResult Handle(
//        [AsParameters] LogInCallbackEndpointParams request,
//        CancellationToken cancellationToken)
//    {
//        var properties = new AuthenticationProperties
//        {
//            RedirectUri = BuildReturnUrl(request.ReturnUrl),
//        };

//        return TypedResults.Challenge(
//            properties,
//            authenticationSchemes: [
//                OpenIddictClientAspNetCoreDefaults.AuthenticationScheme
//            ]);
//    }

//    private static string BuildReturnUrl(
//        string? returnUrl)
//    {
//        const string pathBase = "/";

//        if (string.IsNullOrEmpty(returnUrl))
//        {
//            return pathBase;
//        }

//        if (!Uri.IsWellFormedUriString(returnUrl, UriKind.Relative))
//        {
//            var uri = new Uri(
//                returnUrl,
//                UriKind.Absolute);

//            return uri.PathAndQuery;
//        }

//        if (returnUrl[0] != '/')
//        {
//            return $"{pathBase}{returnUrl}";
//        }

//        return returnUrl;
//    }
//}
