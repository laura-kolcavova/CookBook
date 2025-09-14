import React, { useEffect, useState } from 'react';
import { useAtom } from 'jotai';
import {
  descriptionAtom,
  notesAtom,
  titleAtom,
  ingredientsAtom,
  instructionsAtom,
  tagsAtom,
  recipeDataAtom,
} from './atoms/recipeDataAtom';
import { useSaveRecipeMutation } from './hooks/useSaveRecipeMutation';
import { ErrorAlert } from '~/sharedComponents/alerts/ErrorAlert';
import { useRecipeValidator } from './hooks/useRecipeValidator';
import { FeedbackError } from '~/sharedComponents/forms/FeedbackError';
import { LoadingSpinner } from '~/sharedComponents/LoadingSpinner';
import { useRouter } from '~/navigation/hooks/useRouter';
import { Pages } from '~/navigation/pages';
import { PreparationTimeSetter } from '~/pages/RecipeEditor/PreparationTimeSetter';
import { IngredientsInput } from '~/pages/RecipeEditor/IngredientsInput';
import { InstructionsInput } from '~/pages/RecipeEditor/InstructionsInput';
import { TagsInput } from '~/pages/RecipeEditor/TagsInput';
import { useResetAtom } from 'jotai/utils';
import { areValid } from '~/utils/forms/fieldValidationUtils';
import { Button } from '~/sharedComponents/Button';
import type { FieldValidations } from '~/models/forms/FieldValidations';
import { FormLabel } from '~/sharedComponents/forms/FormLabel';
import { FormTextInput } from '~/sharedComponents/forms/FormTextInput';
import { ServingsSetter } from './ServingsSetter';
import { CookTimeSetter } from './CookingTimeSetter';

export const RecipeEditor: React.FC = () => {
  const { goToPage } = useRouter();

  const [title, setTitle] = useAtom(titleAtom);
  const [description, setDescription] = useAtom(descriptionAtom);
  const [notes, setNotes] = useAtom(notesAtom);

  const [ingredients, setIngredients] = useAtom(ingredientsAtom);
  const [instructions, setInstructions] = useAtom(instructionsAtom);
  const [tags, setTags] = useAtom(tagsAtom);

  const resetRecipeData = useResetAtom(recipeDataAtom);

  const { mutate, isPending, isError, isSuccess, error, data } = useSaveRecipeMutation();

  const { validate } = useRecipeValidator();

  const [validations, setValidations] = useState<FieldValidations>({});

  useEffect(() => {
    if (isSuccess && data) {
      resetRecipeData();

      goToPage(Pages.RecipeDetail, {
        params: { recipeId: data.recipeId.toString() },
      });
    }
  }, [isSuccess, data, goToPage, resetRecipeData]);

  const handleCreateRecipeClick = () => {
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
        <div></div>
        <h2 className="text-2xl font-semibold mb-6">Add Recipe</h2>

        {isPending ? (
          <div className="flex items-center justify-center">
            <LoadingSpinner />
          </div>
        ) : (
          <>
            {isError && <ErrorAlert error={error} />}

            <form className="w-full max-w-3xl">
              <div className="mb-6">
                <FormLabel htmlFor="title">Recipe Title *</FormLabel>

                <FormTextInput
                  id="title"
                  type="text"
                  placeholder="Give your recipe a name"
                  value={title}
                  onChange={(e) => setTitle(e.target.value)}
                  autoComplete="off"
                  // invalid={validations.title?.isValid === false}
                  required
                />

                {validations.title?.errorMessage && (
                  <FeedbackError message={validations.title.errorMessage} />
                )}
              </div>

              <div className="mb-6">
                <FormLabel htmlFor="description">Description</FormLabel>

                <FormTextInput
                  id="description"
                  type="text"
                  placeholder="Introduce your recipe"
                  value={description || ''}
                  onChange={(e) => setDescription(e.target.value || undefined)}
                  autoComplete="off"
                />

                {validations.description?.errorMessage && (
                  <FeedbackError message={validations.description.errorMessage} />
                )}
              </div>

              <div className="mb-6">
                <FormLabel htmlFor="notes">Notes</FormLabel>

                <FormTextInput
                  id="notes"
                  type="textarea"
                  // rows={4}
                  placeholder="Any special tips or notes for this recipe..."
                  value={notes || ''}
                  onChange={(e) => setNotes(e.target.value || undefined)}
                  autoComplete="off"
                />

                {validations.notes?.errorMessage && (
                  <FeedbackError message={validations.notes.errorMessage} />
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
                <IngredientsInput
                  ingredients={ingredients}
                  onChange={setIngredients}
                  label="Ingredients"
                />

                {validations.ingredients?.errorMessage && (
                  <FeedbackError message={validations.ingredients.errorMessage} />
                )}
              </div>

              <div className="mb-6">
                <InstructionsInput
                  instructions={instructions}
                  onChange={setInstructions}
                  label="Instructions"
                />

                {validations.instructions?.errorMessage && (
                  <FeedbackError message={validations.instructions.errorMessage} />
                )}
              </div>

              <div className="mb-12">
                <TagsInput value={tags} onChange={setTags} label="Tags" />
              </div>

              <div>
                <Button
                  color="primary"
                  // size="lg"
                  onClick={handleCreateRecipeClick}
                  disabled={isPending}>
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
