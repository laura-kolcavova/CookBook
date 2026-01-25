import { forwardRef, useImperativeHandle } from 'react';
import { FormattedMessage } from 'react-intl';
import { LoadingSpinner } from '~/pages/shared/LoadingSpinner';
import { useSearchRecipesQuery } from './hooks/useSearchRecipesQuery';
import { Alert } from '~/pages/shared/Alert';
import { Button } from '~/pages/shared/Button';
import { messages } from '../../messages';
import { RecipeSearchItemCard } from './shared/RecipeSearchItemCard';

export type RecipesSearchGridProps = {
  searchTerm: string;
};

export type RecipesSearchGridRef = {
  reload: () => void;
};

export const RecipesSearchGrid = forwardRef<RecipesSearchGridRef, RecipesSearchGridProps>(
  ({ searchTerm }, ref) => {
    const { isFetching, isError, data, hasNextPage, fetchNextPage, resetAndRefetch } =
      useSearchRecipesQuery(searchTerm);

    useImperativeHandle(ref, () => ({
      reload: resetAndRefetch,
    }));

    if (isFetching) {
      return (
        <div className="flex items-center justify-center py-20">
          <LoadingSpinner text={<FormattedMessage {...messages.searching} />} />
        </div>
      );
    }

    if (isError) {
      return (
        <Alert color="danger">
          <FormattedMessage {...messages.searchError} />
        </Alert>
      );
    }

    const allRecipes = data?.pages.flatMap((page) => page.recipes) ?? [];

    if (allRecipes.length === 0) {
      return (
        <p className="text-base text-center text-text-color-secondary py-4">
          {searchTerm ? (
            <FormattedMessage {...messages.noRecipesFound} values={{ searchTerm }} />
          ) : (
            <FormattedMessage {...messages.noRecipesCreatedYet} />
          )}
        </p>
      );
    }

    return (
      <>
        <div className="grid grid-cols-1 sm:grid-cols-2 md:grid-cols-4 gap-x-8 gap-y-12">
          {allRecipes.map((recipe) => (
            <RecipeSearchItemCard key={recipe.recipeId} recipe={recipe} />
          ))}
        </div>

        {hasNextPage && (
          <div className="flex justify-center mt-8">
            <Button onClick={() => fetchNextPage()} disabled={isFetching}>
              <FormattedMessage {...messages.showMoreButton} />
            </Button>
          </div>
        )}
      </>
    );
  },
);

RecipesSearchGrid.displayName = 'RecipesSearchGrid';
