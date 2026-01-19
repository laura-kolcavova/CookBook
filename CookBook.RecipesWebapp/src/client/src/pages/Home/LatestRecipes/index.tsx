import { LoadingSpinner } from '~/pages/shared/LoadingSpinner';
import { useLatestRecipesQuery } from './hooks/useGetLatestRecipesQuery';
import { Alert } from '~/pages/shared/Alert';
import { LatestRecipeCard } from './shared/LatestRecipeCard';

export const LatestRecipes = () => {
  const { isLoading, isError, data } = useLatestRecipesQuery();

  return (
    <div className="bg-content-background-color-tertiary">
      <div className="container mx-auto py-10 px-4">
        <h2 className="text-3xl mb-6 text-center font-handwritten">Latest Recipes</h2>

        {isLoading ? (
          <div className="flex items-center justify-center py-20">
            <LoadingSpinner text="Loading..." />
          </div>
        ) : isError ? (
          <Alert color="danger">Something went wrong</Alert>
        ) : !data || data.latestRecipes.length === 0 ? (
          <p className="text-base text-text-color-secondary py-4">No recipe were created yet.</p>
        ) : (
          <div className="flex flex-col gap-6">
            {data.latestRecipes.map((recipe) => (
              <LatestRecipeCard key={recipe.recipeId} recipe={recipe} />
            ))}
          </div>
        )}
      </div>
    </div>
  );
};
