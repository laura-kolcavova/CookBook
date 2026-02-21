import { useParams } from 'react-router-dom';
import { FormattedMessage } from 'react-intl';
import { RecipeEditor } from '../shared/RecipeEditor';
import { useMemo } from 'react';
import { LoadingSpinner } from '../shared/LoadingSpinner';
import { Alert } from '../shared/Alert';
import { messages } from './messages';
import { sharedMessages } from '../shared/sharedMessages';
import { useGetRecipeDetailQuery } from './hooks/useGetRecipeDetailQuery';

export const EditRecipe = () => {
  const { recipeId: recipeIdParam } = useParams();

  if (recipeIdParam === undefined) {
    throw new Error('Recipe ID is missing in the URL.');
  }

  const recipeId = useMemo(() => Number.parseInt(recipeIdParam, 10), [recipeIdParam]);

  const { isLoading, isError, data } = useGetRecipeDetailQuery(recipeId);

  return (
    <div className="bg-content-background-color-primary">
      <div className="container mx-auto py-10 px-4">
        {isLoading ? (
          <div className="flex items-center justify-center py-20">
            <LoadingSpinner text={<FormattedMessage {...sharedMessages.loading} />} />
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
          <>
            <h2 className="text-2xl font-semibold text-text-color-primary mb-6">
              <FormattedMessage {...messages.editRecipeTitle} />
            </h2>

            <RecipeEditor recipe={data.recipeDetail} />
          </>
        )}
      </div>
    </div>
  );
};
