import React from 'react';
import { Button, Form, FormGroup, Input, Label, UncontrolledAlert } from 'reactstrap';
import { useAtom } from 'jotai';
import {
  cookTimeAtom,
  descriptionAtom,
  notesAtom,
  preparationTimeAtom,
  servingsAtom,
  titleAtom,
} from './atoms/recipeDataAtom';
import { useSaveRecipeMutation } from './hooks/useSaveRecipeMutation';
import { AxiosErrorAlert } from '~/sharedComponents/AxiosErrorAlert';

export const RecipeEditor: React.FC = () => {
  const [title, setTitle] = useAtom(titleAtom);
  const [description, setDescription] = useAtom(descriptionAtom);
  const [notes, setNotes] = useAtom(notesAtom);
  const [servings, setServings] = useAtom(servingsAtom);
  const [preparationTime, setPreparationTime] = useAtom(preparationTimeAtom);
  const [cookTime, setCookTime] = useAtom(cookTimeAtom);

  const { mutateAsync: mutateSaveRecipeAsync, isError, error } = useSaveRecipeMutation();

  const handleCreateRecipeClick = async () => {
    await mutateSaveRecipeAsync();
  };

  console.log(error);

  return (
    <>
      <h2>Add recipe</h2>

      {isError && <AxiosErrorAlert error={error} />}

      <Form>
        <FormGroup>
          <Label for="title">Title</Label>
          <Input
            id="title"
            type="text"
            placeholder="Give your recipe a name"
            value={title}
            onChange={(value) => setTitle(value.currentTarget.value)}
            autocomplete="off"
          />
        </FormGroup>

        <FormGroup>
          <Label for="description">Description</Label>
          <Input
            id="description"
            type="text"
            placeholder="Introduce your recipe"
            value={description}
            onChange={(value) => setDescription(value.currentTarget.value)}
            autocomplete="off"
          />
        </FormGroup>

        <FormGroup>
          <Label for="notes">Notes</Label>
          <Input
            id="notes"
            type="textarea"
            placeholder="Introduce your recipe"
            value={notes}
            onChange={(value) => setNotes(value.currentTarget.value)}
            autocomplete="off"
          />
        </FormGroup>

        <FormGroup>
          <Label for="servings">Servings</Label>
          <Input
            id="servings"
            type="number"
            value={servings}
            onChange={(e) => setServings(Number(e.currentTarget.value))}
          />
        </FormGroup>

        <FormGroup>
          <Label for="preparationTime">Preparation Time (minutes)</Label>
          <Input
            id="preparationTime"
            type="number"
            value={preparationTime}
            onChange={(e) => setPreparationTime(Number(e.currentTarget.value))}
          />
        </FormGroup>

        <FormGroup>
          <Label for="cookTime">Cook Time (minutes)</Label>
          <Input
            id="cookTime"
            type="number"
            value={cookTime}
            onChange={(e) => setCookTime(Number(e.currentTarget.value))}
          />
        </FormGroup>

        <Button onClick={handleCreateRecipeClick}>Create</Button>
      </Form>
    </>
  );
};
