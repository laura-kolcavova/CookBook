import { useMemo } from 'react';
import { useParams } from 'react-router-dom';
import { useRecipeDetailQuery } from './hooks/useGetRecipeDetailQuery';
import { LoadingSpinner } from '../shared/LoadingSpinner';
import { Alert } from '../shared/Alert';
import { RecipeDetailContent } from './shared/RecipeDetailContent';

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
        {isLoading ? (
          <div className="flex items-center justify-center py-20">
            <LoadingSpinner text="Loading..." />
          </div>
        ) : isError ? (
          <Alert color="danger">Something went wrong</Alert>
        ) : !data ? (
          <Alert color="warning">
            <span>No recipe found.</span>
          </Alert>
        ) : (
          <RecipeDetailContent recipe={data.recipeDetail} />
        )}
      </div>
    </div>
  );
};
