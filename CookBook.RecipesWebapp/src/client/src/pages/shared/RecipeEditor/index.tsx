import { useEffect } from 'react';
import { ServingsSetter } from './setters/ServingsSetter';
import { CookTimeSetter } from './setters/CookingTimeSetter';
import { TagsSetter } from './setters/TagsSetters';
import { TitleSetter } from './setters/TitleSetter';
import { DescriptionSetter } from './setters/DescriptionSetter';
import { NotesSetter } from './setters/NotesSetter';
import { IngredientsSetter } from './setters/IngredientsSetter';
import { useNavigate } from 'react-router-dom';
import { useSaveRecipeErrorMessage } from './hooks/useSaveRecipeErrorMessage';
import { pages } from '~/navigation/pages';
import { Alert } from '../Alert';
import { FeedbackError } from '../forms/FeedbackError';
import { InstructionsSetter } from './setters/InstructionsSetter';
import { Button } from '../Button';
import { SpinnerIcon } from '../icons/SpinnerIcon';
import type { RecipeDetailDto } from '~/api/recipes/dto/RecipeDetailDto';
import { useRecipeData } from './hooks/useRecipeData';
import { useSaveRecipeSubmitHandler } from './hooks/useSaveRecipeSubmitHander';

export type RecipeEditorProps = {
  recipe?: RecipeDetailDto;
};

export const RecipeEditor = ({ recipe }: RecipeEditorProps) => {
  const navigate = useNavigate();

  const { initializeDataFromRecipe, resetData, dataInitializedFromRecipe } = useRecipeData();

  const {
    validations,
    saveRecipeIsPending,
    saveRecipeIsSuccess,
    saveRecipeIsError,
    saveRecipeData,
    saveRecipeError,
    handleSubmit,
  } = useSaveRecipeSubmitHandler();

  const { getErrorMessage } = useSaveRecipeErrorMessage();

  useEffect(() => {
    if (!dataInitializedFromRecipe && recipe) {
      initializeDataFromRecipe(recipe);
    }
  }, [dataInitializedFromRecipe, initializeDataFromRecipe, recipe]);

  useEffect(() => {
    return () => {
      resetData();
    };
  }, [resetData]);

  useEffect(() => {
    if (saveRecipeIsSuccess && saveRecipeData) {
      resetData();

      const recipeDetailPath = pages.RecipeDetail.paths[0].replace(
        ':recipeId',
        saveRecipeData.recipeId.toString(),
      );

      navigate(recipeDetailPath);
    }
  }, [navigate, resetData, saveRecipeData, saveRecipeIsSuccess]);

  return (
    <>
      {saveRecipeIsError && (
        <Alert color="danger" isDismissible={true}>
          {getErrorMessage(saveRecipeError!)}
        </Alert>
      )}

      <form className="w-full max-w-3xl" onSubmit={handleSubmit}>
        <div className="mb-6">
          <TitleSetter />

          {validations.title?.errorMessage && (
            <FeedbackError message={validations.title.errorMessage} />
          )}
        </div>

        <div className="mb-6">
          <DescriptionSetter />

          {validations.description?.errorMessage && (
            <FeedbackError message={validations.description.errorMessage} />
          )}
        </div>

        <div className="mb-6">
          <ServingsSetter />

          {validations.servings?.errorMessage && (
            <FeedbackError message={validations.servings.errorMessage} />
          )}
        </div>

        <div className="mb-6">
          <CookTimeSetter />

          {validations.cookTime?.errorMessage && (
            <FeedbackError message={validations.cookTime.errorMessage} />
          )}
        </div>

        <div className="mb-6">
          <IngredientsSetter />

          {validations.ingredients?.errorMessage && (
            <FeedbackError message={validations.ingredients.errorMessage} />
          )}
        </div>

        <div className="mb-6">
          <InstructionsSetter />

          {validations.instructions?.errorMessage && (
            <FeedbackError message={validations.instructions.errorMessage} />
          )}
        </div>

        <div className="mb-6">
          <NotesSetter />

          {validations.notes?.errorMessage && (
            <FeedbackError message={validations.notes.errorMessage} />
          )}
        </div>

        <div className="mb-12">
          <TagsSetter />
        </div>

        <div>
          <Button
            type="submit"
            variant="primary"
            className="w-40 flex items-center justify-center"
            disabled={saveRecipeIsPending}>
            <span>Save</span>

            {saveRecipeIsPending && <SpinnerIcon className="animate-spin size-4 ml-2" />}
          </Button>
        </div>
      </form>
    </>
  );
};
