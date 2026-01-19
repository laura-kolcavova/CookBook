import { LoadingSpinner } from '~/pages/shared/LoadingSpinner';
import { useSearchRecipesQuery } from './hooks/useSearchRecipesQuery';
import { Alert } from '~/pages/shared/Alert';
import { RecipeSearchItemCard } from './RecipeSearchItemCard';

export type RecipesSearchGridProps = {
  searchTerm: string;
};

export const RecipesSearchGrid = ({ searchTerm }: RecipesSearchGridProps) => {
  const { isLoading, isError, data } = useSearchRecipesQuery(searchTerm);

  return isLoading ? (
    <div className="flex items-center justify-center py-20">
      <LoadingSpinner text="Searching..." />
    </div>
  ) : isError ? (
    <Alert color="danger">Something went wrong while searching for recipes</Alert>
  ) : !data || data.recipes.length === 0 ? (
    <p className="text-base text-center text-text-color-secondary py-4">
      No recipes found matching &quot;{searchTerm}&quot;
    </p>
  ) : (
    <div className="flex flex-col gap-6">
      {data.recipes.map((recipe) => (
        <RecipeSearchItemCard key={recipe.recipeId} recipe={recipe} />
      ))}
    </div>
  );
};
