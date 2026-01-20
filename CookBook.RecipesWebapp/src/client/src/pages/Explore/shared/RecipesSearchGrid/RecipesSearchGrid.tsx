import { forwardRef, useImperativeHandle } from 'react';
import { LoadingSpinner } from '~/pages/shared/LoadingSpinner';
import { useSearchRecipesQuery } from './hooks/useSearchRecipesQuery';
import { Alert } from '~/pages/shared/Alert';
import { RecipeSearchItemCard } from './RecipeSearchItemCard';

export type RecipesSearchGridProps = {
  searchTerm: string;
};

export type RecipesSearchGridRef = {
  refetch: () => void;
};

export const RecipesSearchGrid = forwardRef<RecipesSearchGridRef, RecipesSearchGridProps>(
  ({ searchTerm }, ref) => {
    const { isFetching, isError, data, refetch } = useSearchRecipesQuery(searchTerm);

    useImperativeHandle(ref, () => ({
      refetch,
    }));

    return isFetching ? (
      <div className="flex items-center justify-center py-20">
        <LoadingSpinner text="Searching..." />
      </div>
    ) : isError ? (
      <Alert color="danger">Something went wrong while searching for recipes</Alert>
    ) : !data || data.recipes.length === 0 ? (
      <p className="text-base text-center text-text-color-secondary py-4">
        {searchTerm ? `No recipes found matching "${searchTerm}"` : 'No recipes were created yet'}
      </p>
    ) : (
      <div className="grid grid-cols-1 sm:grid-cols-2 md:grid-cols-4 gap-x-8 gap-y-12">
        {data.recipes.map((recipe) => (
          <RecipeSearchItemCard key={recipe.recipeId} recipe={recipe} />
        ))}
      </div>
    );
  },
);

RecipesSearchGrid.displayName = 'RecipesSearchGrid';
