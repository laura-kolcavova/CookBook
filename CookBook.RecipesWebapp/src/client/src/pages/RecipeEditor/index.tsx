import React, { useEffect, useState } from 'react';
import { useAtom } from 'jotai';
import {
  cookTimeAtom,
  descriptionAtom,
  notesAtom,
  preparationTimeAtom,
  servingsAtom,
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
import { LoadingSpinnerWrapper } from './styled';
import { useRouter } from '~/navigation/hooks/useRouter';
import { Pages } from '~/navigation/pages';
import { TimeInput } from '~/pages/RecipeEditor/TimeInput';
import { ServingsInput } from '~/pages/RecipeEditor/ServingsInput';
import { IngredientsInput } from '~/pages/RecipeEditor/IngredientsInput';
import { InstructionsInput } from '~/pages/RecipeEditor/InstructionsInput';
import { TagsInput } from '~/pages/RecipeEditor/TagsInput';
import { useResetAtom } from 'jotai/utils';
import { areValid } from '~/utils/forms/fieldValidationUtils';
import { FormGroup } from '~/sharedComponents/forms/FormGroup';
import { Form } from '~/sharedComponents/forms/Form';
import { Label } from '~/sharedComponents/forms/Label';
import { Input } from '~/sharedComponents/forms/Input';
import { Button } from '~/sharedComponents/forms/Button';
import type { FieldValidations } from '~/models/forms/FieldValidations';

export const RecipeEditor: React.FC = () => {
  const { goToPage } = useRouter();

  const [title, setTitle] = useAtom(titleAtom);
  const [description, setDescription] = useAtom(descriptionAtom);
  const [notes, setNotes] = useAtom(notesAtom);
  const [servings, setServings] = useAtom(servingsAtom);
  const [preparationTime, setPreparationTime] = useAtom(preparationTimeAtom);
  const [cookTime, setCookTime] = useAtom(cookTimeAtom);
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
    <>
      <h2>Add Recipe</h2>

      {isPending ? (
        <LoadingSpinnerWrapper>
          <LoadingSpinner />
        </LoadingSpinnerWrapper>
      ) : (
        <>
          {isError && <ErrorAlert error={error} />}

          <Form>
            {/* form group */}
            <FormGroup>
              <Label htmlFor="title">Recipe Title *</Label>

              <Input
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
            </FormGroup>

            <FormGroup>
              <Label htmlFor="description">Description</Label>

              <Input
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
            </FormGroup>

            <FormGroup>
              <Label htmlFor="notes">Notes</Label>

              <Input
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
            </FormGroup>

            <FormGroup>
              <ServingsInput value={servings} onChange={setServings} label="Number of Servings" />

              {validations.servings?.errorMessage && (
                <FeedbackError message={validations.servings.errorMessage} />
              )}
            </FormGroup>

            <FormGroup>
              <TimeInput
                valueInMinutes={preparationTime}
                onChange={setPreparationTime}
                label="Preparation Time"
              />

              {validations.preparationTime?.errorMessage && (
                <FeedbackError message={validations.preparationTime.errorMessage} />
              )}
            </FormGroup>

            <FormGroup>
              <TimeInput valueInMinutes={cookTime} onChange={setCookTime} label="Cooking Time" />

              {validations.cookTime?.errorMessage && (
                <FeedbackError message={validations.cookTime.errorMessage} />
              )}
            </FormGroup>

            <FormGroup>
              <IngredientsInput
                ingredients={ingredients}
                onChange={setIngredients}
                label="Ingredients"
              />

              {validations.ingredients?.errorMessage && (
                <FeedbackError message={validations.ingredients.errorMessage} />
              )}
            </FormGroup>

            <FormGroup>
              <InstructionsInput
                instructions={instructions}
                onChange={setInstructions}
                label="Instructions"
              />

              {validations.instructions?.errorMessage && (
                <FeedbackError message={validations.instructions.errorMessage} />
              )}
            </FormGroup>

            <FormGroup>
              <TagsInput value={tags} onChange={setTags} label="Tags" />
            </FormGroup>

            <div className="d-flex justify-content-between align-items-center mt-4">
              <Button
                color="primary"
                // size="lg"
                onClick={handleCreateRecipeClick}
                disabled={isPending}>
                Create Recipe
              </Button>
            </div>
          </Form>
        </>
      )}
    </>
  );
};
