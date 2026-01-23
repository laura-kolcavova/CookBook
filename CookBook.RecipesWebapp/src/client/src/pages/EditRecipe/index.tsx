import { useParams } from 'react-router-dom';
import { RecipeEditor } from '../shared/RecipeEditor';
import { useMemo } from 'react';
import { useRecipeDetailQuery } from './hooks/useGetRecipeDetailQuery';
import { LoadingSpinner } from '../shared/LoadingSpinner';
import { Alert } from '../shared/Alert';

export const EditRecipe = () => {
  const { recipeId: recipeIdParam } = useParams();

  if (recipeIdParam === undefined) {
    throw new Error('Recipe ID is missing in the URL.');
  }

  const recipeId = useMemo(() => Number.parseInt(recipeIdParam, 10), [recipeIdParam]);

  const { isLoading, isError, data } = useRecipeDetailQuery(recipeId);

  return (
    <div className="bg-content-background-color-primary">
      <div className="container mx-auto py-10 px-4">
        {isLoading ? (
          <div className="flex items-center justify-center py-20">
            <LoadingSpinner text="Loading..." />
          </div>
        ) : isError ? (
          <Alert color="danger">Something went wrong</Alert>
        ) : !data ? (
          <p className="text-base text-center text-text-color-secondary py-4">Recipe not found</p>
        ) : (
          <>
            <h2 className="text-2xl font-semibold text-text-color-primary mb-6">Edit Recipe</h2>

            <RecipeEditor recipe={data.recipeDetail} />
          </>
        )}
      </div>
    </div>
  );
};
