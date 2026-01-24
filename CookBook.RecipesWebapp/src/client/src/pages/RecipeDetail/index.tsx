import { useMemo } from 'react';
import { FormattedMessage } from 'react-intl';
import { useParams } from 'react-router-dom';
import { useRecipeDetailQuery } from './hooks/useGetRecipeDetailQuery';
import { LoadingSpinner } from '../shared/LoadingSpinner';
import { Alert } from '../shared/Alert';
import { RecipeDetailContent } from './shared/RecipeDetailContent';
import { RecipeDetailHeader } from './shared/RecipeDetailHeader';
import { messages } from './messages';
import { sharedMessages } from '../shared/sharedMessages';

export const RecipeDetail = () => {
  const { recipeId: recipeIdParam } = useParams();

  if (recipeIdParam === undefined) {
    throw new Error('Recipe ID is missing in the URL.');
  }

  const recipeId = useMemo(() => Number.parseInt(recipeIdParam, 10), [recipeIdParam]);

  const { isLoading, isError, data } = useRecipeDetailQuery(recipeId);

  return (
    <div className="bg-content-background-color-tertiary">
      <div className="container mx-auto py-10 px-4">
        <RecipeDetailHeader recipe={data?.recipeDetail} />

        {isLoading ? (
          <div className="flex items-center justify-center py-20">
            <LoadingSpinner text="Loading..." />
          </div>
        ) : isError ? (
          <Alert color="danger">
            <FormattedMessage {...sharedMessages.somethingWentWrong} />
          </Alert>
        ) : !data ? (
          <p className="text-base text-center text-text-color-secondary py-4">
            <FormattedMessage {...messages.recipeNotFound} />
          </p>
        ) : (
          <RecipeDetailContent recipe={data.recipeDetail} />
        )}
      </div>
    </div>
  );
};
