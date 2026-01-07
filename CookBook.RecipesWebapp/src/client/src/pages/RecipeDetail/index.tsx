import { useMemo } from 'react';
import { useParams } from 'react-router-dom';
import { useRecipeDetailQuery } from './hooks/useGetRecipeDetailQuery';
import { LoadingSpinner } from '../shared/LoadingSpinner';
import { ErrorAlert } from '../shared/alerts/ErrorAlert';
import { Alert } from '../shared/alerts/Alert';
import { MetaItem } from './shared/MetaItem';
import { MetaLabel } from './shared/MetaLabel';
import { MetaValue } from './shared/MetaValue';

export const RecipeDetail = () => {
  const { recipeId: recipeIdParam } = useParams();

  if (recipeIdParam === undefined) {
    throw new Error('Recipe ID is missing in the URL.');
  }

  const recipeId = useMemo(() => Number.parseInt(recipeIdParam, 10), [recipeIdParam]);

  const { isLoading, isError, data, error } = useRecipeDetailQuery(recipeId);

  const formatTime = (minutes: number): string => {
    if (minutes < 60) {
      return `${minutes} min`;
    }
    const hours = Math.floor(minutes / 60);
    const remainingMinutes = minutes % 60;
    return remainingMinutes > 0 ? `${hours}h ${remainingMinutes}min` : `${hours}h`;
  };

  return isLoading ? (
    <div className="flex items-center justify-center">
      <LoadingSpinner />
    </div>
  ) : isError ? (
    <ErrorAlert error={error} />
  ) : !data ? (
    <Alert color="warning">
      <span>No recipe found.</span>
    </Alert>
  ) : (
    <div className="p-8">
      <div className="mb-8 border-b-2 border-gray-200 pb-6">
        <h1 className="text-gray-900 mb-4 text-4xl font-bold">{data.recipeDetail.title}</h1>

        {data.recipeDetail.description && (
          <p className="text-gray-600 text-lg leading-relaxed mb-4">
            {data.recipeDetail.description}
          </p>
        )}

        <div className="grid grid-cols-[repeat(auto-fit,minmax(150px,1fr))] gap-4 mb-6">
          <MetaItem>
            <MetaLabel>Servings</MetaLabel>
            <MetaValue>{data.recipeDetail.servings}</MetaValue>
          </MetaItem>

          <MetaItem>
            <MetaLabel>Prep Time</MetaLabel>
            <MetaValue>{formatTime(data.recipeDetail.preparationTime)}</MetaValue>
          </MetaItem>

          <MetaItem>
            <MetaLabel>Cook Time</MetaLabel>
            <MetaValue>{formatTime(data.recipeDetail.cookTime)}</MetaValue>
          </MetaItem>

          <MetaItem>
            <MetaLabel>Total Time</MetaLabel>
            <MetaValue>
              {formatTime(data.recipeDetail.preparationTime + data.recipeDetail.cookTime)}
            </MetaValue>
          </MetaItem>
        </div>

        {data.recipeDetail.tags.length > 0 && (
          <div className="mb-8">
            <div className="flex flex-wrap gap-2">
              {data.recipeDetail.tags.map((tag, index) => (
                <span
                  key={index}
                  className="bg-[var(--navbar-background-color)] text-[var(--text-primary-color)] px-4 py-2 rounded-full text-sm font-medium">
                  {tag}
                </span>
              ))}
            </div>
          </div>
        )}
      </div>

      <div className="mb-8">
        <h2 className="text-gray-900 mb-4 text-3xl font-semibold border-l-4 border-[var(--navbar-background-color)] pl-4">
          Ingredients
        </h2>

        <ul className="list-none p-0">
          {data.recipeDetail.ingredients.map((ingredient) => (
            <li
              key={ingredient.localId}
              className="bg-gray-100 mb-2 p-4 rounded-lg border-l-4 border-[var(--navbar-background-color)] text-base leading-6">
              {ingredient.note}
            </li>
          ))}
        </ul>
      </div>

      <div className="mb-8">
        <h2 className="text-gray-900 mb-4 text-3xl font-semibold border-l-4 border-[var(--navbar-background-color)] pl-4">
          Instructions
        </h2>

        <ol className="p-0 [counter-reset:instruction-counter]">
          {data.recipeDetail.instructions.map((instruction) => (
            <li
              key={instruction.localId}
              className="bg-gray-100 mb-4 p-6 rounded-lg border-l-4 border-[var(--navbar-background-color)] text-base leading-relaxed list-none [counter-increment:instruction-counter] relative before:content-[counter(instruction-counter)] before:absolute before:-left-2 before:top-2 before:bg-[var(--navbar-background-color)] before:text-[var(--text-primary-color)] before:w-8 before:h-8 before:rounded-full before:flex before:items-center before:justify-center before:font-bold before:text-sm">
              {instruction.note}
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
    </div>
  );
};
