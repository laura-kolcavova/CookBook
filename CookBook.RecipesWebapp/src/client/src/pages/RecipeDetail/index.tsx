import { useMemo } from 'react';
import { useParams } from 'react-router-dom';
import { useRecipeDetailQuery } from './hooks/useGetRecipeDetailQuery';
import { LoadingSpinner } from '../shared/LoadingSpinner';
import { ErrorAlert } from '../shared/alerts/ErrorAlert';
import { Alert } from '../shared/alerts/Alert';
import { MetaItem } from './shared/MetaItem';
import { MetaLabel } from './shared/MetaLabel';
import { MetaValue } from './shared/MetaValue';
import { Tag } from '../shared/Tag';
import { useFormatCookTime } from './hooks/useFormatCookTime';
import { useFormatServings } from './hooks/useFormatServings';

export const RecipeDetail = () => {
  const { recipeId: recipeIdParam } = useParams();

  if (recipeIdParam === undefined) {
    throw new Error('Recipe ID is missing in the URL.');
  }

  const recipeId = useMemo(() => Number.parseInt(recipeIdParam, 10), [recipeIdParam]);

  const { isLoading, isError, data, error } = useRecipeDetailQuery(recipeId);

  const formatServings = useFormatServings();
  const formatCookTime = useFormatCookTime();

  return (
    <div className="bg-content-background-color-primary h-full">
      <div className="container mx-auto py-10">
        {isLoading ? (
          <div className="flex items-center justify-center py-20">
            <LoadingSpinner text="Loading..." />
          </div>
        ) : isError ? (
          <ErrorAlert error={error} />
        ) : !data ? (
          <Alert color="warning">
            <span>No recipe found.</span>
          </Alert>
        ) : (
          <>
            <div className="mb-8 border-b-2 border-gray-200 pb-6">
              <h2 className="text-2xl text-center text-color-primary font-semibold mb-6">
                {data.recipeDetail.title}
              </h2>

              {data.recipeDetail.description && (
                <p className="text-lg text-color-secondary mb-4">{data.recipeDetail.description}</p>
              )}

              <div className="grid grid-cols-[repeat(auto-fit,minmax(150px,1fr))] gap-4 mb-6">
                <MetaItem>
                  <MetaLabel>Servings</MetaLabel>
                  <MetaValue>{formatServings(data.recipeDetail.servings)}</MetaValue>
                </MetaItem>

                <MetaItem>
                  <MetaLabel>Cook Time</MetaLabel>
                  <MetaValue>{formatCookTime(data.recipeDetail.cookTime)}</MetaValue>
                </MetaItem>
              </div>

              {data.recipeDetail.tags.length > 0 && (
                <div className="mb-6">
                  <div className="flex flex-wrap gap-2">
                    {data.recipeDetail.tags.map((tag, index) => (
                      <Tag key={index} tag={tag} />
                    ))}
                  </div>
                </div>
              )}
            </div>

            <div className="mb-8">
              <h2 className="text-2xl font-semibold text-color-primary mb-4">Ingredients</h2>

              <ul className="list-none p-0 ml-4">
                {data.recipeDetail.ingredients.map((ingredient) => (
                  <li
                    key={ingredient.localId}
                    className="bg-gray-100 mb-2 p-4 rounded-md border-l-4 border-[var(--text-color-primary)] text-base text-color-primary leading-6">
                    {ingredient.note}
                  </li>
                ))}
              </ul>
            </div>

            <div className="mb-8">
              <h2 className="text-2xl font-semibold text-color-primary mb-4">Instructions</h2>

              <ol className="p-0 ml-4">
                {data.recipeDetail.instructions.map((instruction, index) => (
                  <li
                    key={instruction.localId}
                    className="bg-gray-100 mb-4 p-6 rounded-md relative">
                    <div className="absolute w-6 h-6 -left-1 -top-1 rounded-full flex items-center justify-center text-sm leading-normal font-bold bg-button-background-color-primary text-button-color-primary">
                      {index + 1}
                    </div>

                    <span className="text-base leading-relaxed">{instruction.note}</span>
                  </li>
                ))}
              </ol>
            </div>

            {data.recipeDetail.notes && (
              <div className="bg-yellow-50 border border-yellow-200 rounded-lg p-6 mt-8">
                <h3 className="text-yellow-800 mb-4 text-xl font-semibold">Notes</h3>
                <p className="text-yellow-800 m-0 leading-relaxed">{data.recipeDetail.notes}</p>
              </div>
            )}
          </>
        )}
      </div>
    </div>
  );
};
