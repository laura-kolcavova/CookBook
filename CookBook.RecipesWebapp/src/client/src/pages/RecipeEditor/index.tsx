import React, { useEffect, useState } from 'react';
import { recipeDataAtom } from './atoms/recipeDataAtom';
import { useSaveRecipeMutation } from './hooks/useSaveRecipeMutation';
import { useRecipeValidator } from './hooks/useRecipeValidator';
import { InstructionsSetter } from '~/pages/RecipeEditor/setters/InstructionsSetter';
import { useResetAtom } from 'jotai/utils';
import { areValid } from '~/utils/forms/fieldValidationUtils';
import { ServingsSetter } from './setters/ServingsSetter';
import { CookTimeSetter } from './setters/CookingTimeSetter';
import { TagsSetter } from './setters/TagsSetters';
import { TitleSetter } from './setters/TitleSetter';
import { DescriptionSetter } from './setters/DescriptionSetter';
import { NotesSetter } from './setters/NotesSetter';
import { IngredientsSetter } from './setters/IngredientsSetter';
import { useNavigate } from 'react-router-dom';
import type { FieldValidations } from '~/forms/FieldValidations';
import { FeedbackError } from '../shared/forms/FeedbackError';
import { Button } from '../shared/Button';
import { Alert } from '../shared/Alert';
import { useSaveRecipeErrorMessage } from './hooks/useSaveRecipeErrorMessage';
import { pages } from '~/navigation/pages';
import { SpinnerIcon } from '../shared/icons/SpinnerIcon';

export const RecipeEditor = () => {
  const navigate = useNavigate();

  const resetRecipeData = useResetAtom(recipeDataAtom);

  const [validations, setValidations] = useState<FieldValidations>({});

  const {
    mutate: saveRecipeMutate,
    isPending: saveRecipeIsPending,
    isSuccess: saveRecipeIsSuccess,
    isError: saveRecipeIsError,
    data: saveRecipeData,
    error: saveRecipeError,
  } = useSaveRecipeMutation();

  const { validate } = useRecipeValidator();

  const { getErrorMessage } = useSaveRecipeErrorMessage();

  useEffect(() => {
    if (saveRecipeIsSuccess && saveRecipeData) {
      resetRecipeData();

      const recipeDetailPath = pages.RecipeDetail.paths[0].replace(
        ':recipeId',
        saveRecipeData.recipeId.toString(),
      );

      navigate(recipeDetailPath);
    }
  }, [saveRecipeData, saveRecipeIsSuccess, navigate, resetRecipeData]);

  const handleSubmit = (e: React.FormEvent) => {
    e.preventDefault();

    const validationResults = validate();
    if (!areValid(validationResults)) {
      setValidations(validationResults);
      return;
    }
    setValidations({});
    saveRecipeMutate();
  };

  return (
    <div className="bg-content-background-color-primary">
      <div className="container mx-auto py-10 px-4">
        <h2 className="text-2xl font-semibold text-text-color-primary mb-6">Add Recipe</h2>

        <>
          {saveRecipeIsError && (
            <Alert color="danger" isDismissible={true}>
              {getErrorMessage(saveRecipeError)}
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
                <span>Create Recipe</span>

                {saveRecipeIsPending && <SpinnerIcon className="animate-spin size-4 ml-2" />}
              </Button>
            </div>
          </form>
        </>
      </div>
    </div>
  );
};
