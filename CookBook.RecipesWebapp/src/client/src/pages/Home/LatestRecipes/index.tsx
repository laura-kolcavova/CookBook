import { LoadingSpinner } from '~/pages/shared/LoadingSpinner';
import { useLatestRecipesQuery } from './hooks/useGetLatestRecipesQuery';
import { Alert } from '~/pages/shared/Alert';
import { LatestRecipeCard } from './LatestRecipeCard';

export const LatestRecipes = () => {
  const { isLoading, isError, data } = useLatestRecipesQuery();

  return (
    <div className="bg-content-background-color-tertiary">
      <div className="container mx-auto py-10">
        <h2 className="text-3xl mb-6 text-center font-handwritten">Latest Recipes</h2>

        {isLoading ? (
          <div className="flex items-center justify-center py-20">
            <LoadingSpinner text="Loading..." />
          </div>
        ) : isError ? (
          <Alert color="danger">Something went wrong</Alert>
        ) : !data || data.length === 0 ? (
          <Alert color="warning">
            <span>No recipe were created yet.</span>
          </Alert>
        ) : (
          <div className="grid grid-cols-1 md:grid-cols-3 gap-6">
            {data.map((recipe) => (
              <LatestRecipeCard key={recipe.id} recipe={recipe} />
            ))}
          </div>
        )}
      </div>
    </div>
  );
};
