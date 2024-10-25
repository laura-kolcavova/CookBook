import React from 'react';
import { Button, Form, FormGroup, Input, Label } from 'reactstrap';
import { useAtom } from 'jotai';
import { recipeDescriptionAtom, recipeNotesAtom, recipeTitleAtom } from './atoms';

export const RecipeEditor: React.FC = () => {
  const [recipeTitle, setRecipeTitle] = useAtom(recipeTitleAtom);
  const [recipeDescription, setRecipeDescription] = useAtom(recipeDescriptionAtom);
  const [recipeNotes, setRecipeNotes] = useAtom(recipeNotesAtom);

  const handleCreateRecipeClick = () => {};

  return (
    <>
      <h2>Add recipe</h2>

      <Form>
        <FormGroup>
          <Label for="title">Title</Label>
          <Input
            id="title"
            type="text"
            placeholder="Give your recipe a name"
            value={recipeTitle}
            onChange={(value) => setRecipeTitle(value.currentTarget.value)}
          />
        </FormGroup>

        <FormGroup>
          <Label for="description">Description</Label>
          <Input
            id="description"
            type="text"
            placeholder="Introduce your recipe"
            value={recipeDescription}
            onChange={(value) => setRecipeDescription(value.currentTarget.value)}
          />
        </FormGroup>

        <FormGroup>
          <Label for="notes">Notes</Label>
          <Input
            id="description"
            type="textarea"
            placeholder="Introduce your recipe"
            value={recipeNotes}
            onChange={(value) => setRecipeNotes(value.currentTarget.value)}
          />
        </FormGroup>

        <Button onClick={handleCreateRecipeClick}>Create</Button>
      </Form>
    </>
  );
};
