import React, { useEffect, useState } from 'react';
import { recipeDataAtom } from './atoms/recipeDataAtom';
import { useSaveRecipeMutation } from './hooks/useSaveRecipeMutation';
import { useRecipeValidator } from './hooks/useRecipeValidator';
import { Pages } from '~/navigation/pages';
import { PreparationTimeSetter } from '~/pages/RecipeEditor/setters/PreparationTimeSetter';
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
import { FieldValidations } from '~/forms/FieldValidations';
import { LoadingSpinner } from '../shared/LoadingSpinner';
import { ErrorAlert } from '../shared/alerts/ErrorAlert';
import { FeedbackError } from '../shared/forms/FeedbackError';
import { Button } from '../shared/Button';

export const RecipeEditor: React.FC = () => {
  const navigate = useNavigate();

  const resetRecipeData = useResetAtom(recipeDataAtom);

  const { mutate, isPending, isError, isSuccess, error, data } = useSaveRecipeMutation();

  const { validate } = useRecipeValidator();

  const [validations, setValidations] = useState<FieldValidations>({});

  useEffect(() => {
    if (isSuccess && data) {
      resetRecipeData();

      const recipeDetailPath = Pages.RecipeDetail.paths[0].replace(
        ':recipeId',
        data.recipeId.toString(),
      );

      navigate(recipeDetailPath);
    }
  }, [isSuccess, data, resetRecipeData, navigate]);

  const handleSubmit = (e: React.FormEvent<HTMLFormElement>) => {
    e.preventDefault();

    const validationResults = validate();
    if (!areValid(validationResults)) {
      setValidations(validationResults);
      return;
    }
    setValidations({});
    mutate();
  };

  return (
    <div className="content-background-color-primary">
      <div className="container mx-auto py-10 ">
        <h2 className="text-2xl font-semibold mb-6">Add Recipe</h2>

        {isPending ? (
          <div className="flex items-center justify-center">
            <LoadingSpinner />
          </div>
        ) : (
          <>
            {isError && <ErrorAlert error={error} />}

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
                <PreparationTimeSetter />

                {validations.preparationTime?.errorMessage && (
                  <FeedbackError message={validations.preparationTime.errorMessage} />
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
                <Button type="submit" color="primary" disabled={isPending}>
                  Create Recipe
                </Button>
              </div>
            </form>
          </>
        )}
      </div>
    </div>
  );
};
