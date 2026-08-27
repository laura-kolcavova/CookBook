import { useEffect, useState } from 'react';
import { FormattedMessage } from 'react-intl';
import { Alert } from '../Alert';
import { Button } from '../Button';
import { FeedbackError } from '../forms/FeedbackError';
import { SpinnerIcon } from '../icons/SpinnerIcon';
import { useRecipeData } from './hooks/useRecipeData';
import { useRecipeValidator } from './hooks/useRecipeValidator';
import { useSaveRecipeErrorMessage } from './hooks/useSaveRecipeErrorMessage';
import { useSaveRecipeMutation } from './hooks/useSaveRecipeMutation';
import { messages } from './messages';
import { CookTimeSetter } from './setters/CookingTimeSetter';
import { DescriptionSetter } from './setters/DescriptionSetter';
import { IngredientsSetter } from './setters/IngredientsSetter';
import { InstructionsSetter } from './setters/InstructionsSetter';
import { NotesSetter } from './setters/NotesSetter';
import { ServingsSetter } from './setters/ServingsSetter';
import { TagsSetter } from './setters/TagsSetters';
import { TitleSetter } from './setters/TitleSetter';
import type { RecipeDetailDto } from '~/api/recipes/dto/GetRecipeDetailResponseDto';
import type { FieldValidations } from '~/forms/FieldValidations';
import { areValid } from '~/utils/forms/fieldValidationUtils';

export type RecipeEditorProps = {
  recipe?: RecipeDetailDto;
};

export const RecipeEditor = ({ recipe }: RecipeEditorProps) => {
  const { initializeDataFromRecipe, resetData, dataInitializedFromRecipe } = useRecipeData();

  const {
    mutate: saveRecipeMutate,
    isPending: saveRecipeIsPending,
    isError: saveRecipeIsError,
    error: saveRecipeError,
  } = useSaveRecipeMutation();

  const { validate } = useRecipeValidator();

  const [validations, setValidations] = useState<FieldValidations>({});

  const handleSubmit = (e: React.SubmitEvent<HTMLFormElement>) => {
    e.preventDefault();

    const validationResults = validate();

    setValidations(validationResults);

    if (!areValid(validationResults)) {
      return;
    }

    saveRecipeMutate();
  };

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
            <span>
              <FormattedMessage {...messages.saveButton} />
            </span>

            {saveRecipeIsPending && <SpinnerIcon className="animate-spin size-4 ml-2" />}
          </Button>
        </div>
      </form>
    </>
  );
};
