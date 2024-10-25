using Microsoft.Extensions.Logging;

namespace CookBook.Recipes.Persistence.Shared.Exceptions;

public class RecipesPersistenceException : Exception
{
    public RecipesPersistenceException(string message)
        : base(message)
    {
    }

    public static RecipesPersistenceException LogAndCreate(
        ILogger logger,
        Exception? innerException,
        string message)
    {
        logger.LogError(
            innerException,
            message);

        return new RecipesPersistenceException(message);
    }
}
