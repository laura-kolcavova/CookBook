using CookBook.RecipesWebapp.Server.Domain.Users.Models;

namespace CookBook.RecipesWebapp.Server.Domain.Users.Services.Abstractions;

public interface ICurrentUserProfileFetcher
{
    public Task<CurrentUserProfileModel> FetchCurrentUserProfile(
        CancellationToken cancellationToken);
}
