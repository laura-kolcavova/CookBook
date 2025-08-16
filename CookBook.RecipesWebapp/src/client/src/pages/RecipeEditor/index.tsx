import React, { useEffect } from 'react';
import { Button, Form, FormGroup, Input, Label } from 'reactstrap';
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
} from './atoms/recipeDataAtom';
import { useSaveRecipeMutation } from './hooks/useSaveRecipeMutation';
import { ErrorAlert } from '~/sharedComponents/ErrorAlert';
import { useRecipeValidator } from './hooks/useRecipeValidator';
import { FeedbackError } from '~/sharedComponents/FeedbackError';
import { LoadingSpinner } from '~/sharedComponents/LoadingSpinner';
import { LoadingSpinnerWrapper } from './styled';
import { useRouter } from '~/navigation/hooks/useRouter';
import { Pages } from '~/navigation/pages';
import { TimeInput } from '~/sharedComponents/TimeInput';
import { ServingsInput } from '~/sharedComponents/ServingsInput';
import { IngredientsInput } from '~/sharedComponents/IngredientsInput';
import { InstructionsInput } from '~/sharedComponents/InstructionsInput';
import { TagsInput } from '~/sharedComponents/TagsInput';

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

  const { mutate, isPending, isError, isSuccess, error, data } = useSaveRecipeMutation();

  const { validate, validations, resetValidations } = useRecipeValidator();

  useEffect(() => {
    if (isSuccess && data) {
      goToPage(Pages.RecipeDetail, {
        params: { recipeId: data.recipeId.toString() },
      });
    }
  }, [isSuccess, data, goToPage]);

  const handleCreateRecipeClick = () => {
    if (!validate()) {
      return;
    }

    resetValidations();
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
            <FormGroup>
              <Label for="title">Recipe Title *</Label>
              <Input
                id="title"
                type="text"
                placeholder="Give your recipe a name"
                value={title}
                onChange={(e) => setTitle(e.target.value)}
                autoComplete="off"
                invalid={validations.title?.isValid === false}
                required
              />
              {validations.title?.invalidMessage && (
                <FeedbackError message={validations.title.invalidMessage} />
              )}
            </FormGroup>

            <FormGroup>
              <Label for="description">Description</Label>
              <Input
                id="description"
                type="text"
                placeholder="Introduce your recipe"
                value={description || ''}
                onChange={(e) => setDescription(e.target.value || undefined)}
                autoComplete="off"
              />
            </FormGroup>

            <FormGroup>
              <Label for="notes">Notes</Label>
              <Input
                id="notes"
                type="textarea"
                rows={4}
                placeholder="Any special tips or notes for this recipe..."
                value={notes || ''}
                onChange={(e) => setNotes(e.target.value || undefined)}
                autoComplete="off"
              />
            </FormGroup>

            <FormGroup>
              <ServingsInput value={servings} onChange={setServings} label="Number of Servings" />
            </FormGroup>

            <FormGroup>
              <TimeInput
                valueInMinutes={preparationTime}
                onChange={setPreparationTime}
                label="Preparation Time"
              />
            </FormGroup>

            <FormGroup>
              <TimeInput valueInMinutes={cookTime} onChange={setCookTime} label="Cooking Time" />
            </FormGroup>

            <FormGroup>
              <IngredientsInput value={ingredients} onChange={setIngredients} label="Ingredients" />

              {validations.ingredients?.invalidMessage && (
                <FeedbackError message={validations.ingredients.invalidMessage} />
              )}
            </FormGroup>

            <FormGroup>
              <InstructionsInput
                value={instructions}
                onChange={setInstructions}
                label="Instructions"
              />
              {validations.instructions?.invalidMessage && (
                <FeedbackError message={validations.instructions.invalidMessage} />
              )}
            </FormGroup>

            <FormGroup>
              <TagsInput value={tags} onChange={setTags} label="Tags" />
            </FormGroup>

            <div className="d-flex justify-content-between align-items-center mt-4">
              <Button
                color="primary"
                size="lg"
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
