import type { RecipeDetailDto } from '~/api/recipes/dto/RecipeDetailDto';
import { useRemoveRecipeMutation } from './hooks/useRemoveRecipeMutation';
import { useLoggedUser } from '~/authentication/LoggedUserProvider';
import { useFormatting } from '~/localization/useFormatting';
import { useNavigate } from 'react-router-dom';
import { useFormatServings } from './hooks/useFormatServings';
import { useFormatCookTime } from './hooks/useFormatCookTime';
import { Button } from '~/pages/shared/Button';
import { MetaItem } from './shared/MetaItem';
import { MetaLabel } from './shared/MetaLabel';
import { MetaValue } from './shared/MetaValue';
import { Tag } from '~/pages/shared/Tag';
import { useEffect } from 'react';
import { pages } from '~/navigation/pages';
import { useSaveRemoveErrorMessage } from './hooks/useRemoveRecipeErrorMessage';
import { Alert } from '~/pages/shared/Alert';

export type RecipeDetailContentProps = {
  recipe: RecipeDetailDto;
};
export const RecipeDetailContent = ({ recipe }: RecipeDetailContentProps) => {
  const navigate = useNavigate();

  const { isAuthenticated, user } = useLoggedUser();

  const { formatDate } = useFormatting();

  const formatServings = useFormatServings();
  const formatCookTime = useFormatCookTime();

  const {
    mutate: removeRecipeMutate,
    isSuccess: removeRecieIsSuccess,
    isPending: removeRecipeIsPending,
    isError: removeRecipeIsError,
    error: removeRecipeError,
  } = useRemoveRecipeMutation(recipe.recipeId);

  const { getErrorMessage } = useSaveRemoveErrorMessage();

  const handleDelete = () => {
    if (window.confirm('Are you sure you want to delete this recipe?')) {
      removeRecipeMutate();
    }
  };

  useEffect(() => {
    if (removeRecieIsSuccess) {
      navigate(pages.MyProfile.paths[0]);
    }
  }, [navigate, removeRecieIsSuccess]);

  return (
    <>
      {removeRecipeIsError && (
        <Alert color="danger" isDismissible={true}>
          {getErrorMessage(removeRecipeError)}
        </Alert>
      )}

      <div className="mb-8 border-b-2 border-gray-200 pb-6">
        <div className="flex justify-between items-start mb-6">
          <h2 className="text-2xl text-center text-text-color-primary font-semibold flex-1">
            {recipe.title}
          </h2>

          {isAuthenticated && recipe.userId === user.userId && (
            <Button
              onClick={handleDelete}
              disabled={removeRecipeIsPending}
              className="bg-red-600 hover:bg-red-700 text-white">
              {removeRecipeIsPending ? 'Deleting...' : 'Delete Recipe'}
            </Button>
          )}
        </div>

        <div className="text-center mb-2">
          <span className="text-sm text-text-color-secondary">{formatDate(recipe.createdAt)}</span>
        </div>

        {recipe.description && (
          <p className="text-lg text-text-color-secondary mb-4">{recipe.description}</p>
        )}

        <div className="grid grid-cols-[repeat(auto-fit,minmax(150px,1fr))] gap-4 mb-6">
          <MetaItem>
            <MetaLabel>Servings</MetaLabel>
            <MetaValue>{formatServings(recipe.servings)}</MetaValue>
          </MetaItem>

          <MetaItem>
            <MetaLabel>Cook Time</MetaLabel>
            <MetaValue>{formatCookTime(recipe.cookTime)}</MetaValue>
          </MetaItem>
        </div>

        {recipe.tags.length > 0 && (
          <div className="mb-6">
            <div className="flex flex-wrap gap-2">
              {recipe.tags.map((tag, index) => (
                <Tag key={index} tag={tag} />
              ))}
            </div>
          </div>
        )}
      </div>

      <div className="mb-8">
        <h2 className="text-2xl font-semibold text-text-color-primary mb-4">Ingredients</h2>

        <ul className="list-none p-0 ml-4">
          {recipe.ingredients.map((ingredient) => (
            <li
              key={ingredient.localId}
              className="bg-gray-100 mb-2 p-4 rounded-md border-l-4 border-[var(--text-text-color-primary)] text-base text-text-color-primary leading-6">
              {ingredient.note}
            </li>
          ))}
        </ul>
      </div>

      <div className="mb-8">
        <h2 className="text-2xl font-semibold text-text-color-primary mb-4">Instructions</h2>

        <ol className="p-0 ml-4">
          {recipe.instructions.map((instruction, index) => (
            <li key={instruction.localId} className="bg-gray-100 mb-4 p-6 rounded-md relative">
              <div className="absolute w-6 h-6 -left-1 -top-1 rounded-full flex items-center justify-center text-sm leading-normal font-bold bg-button-background-color-primary text-button-color-primary">
                {index + 1}
              </div>

              <span className="text-base leading-relaxed">{instruction.note}</span>
            </li>
          ))}
        </ol>
      </div>

      {recipe.notes && (
        <div className="bg-yellow-50 border border-yellow-200 rounded-lg p-6 mt-8">
          <h3 className="text-yellow-800 mb-4 text-xl font-semibold">Notes</h3>
          <p className="text-yellow-800 m-0 leading-relaxed">{recipe.notes}</p>
        </div>
      )}
    </>
  );
};
