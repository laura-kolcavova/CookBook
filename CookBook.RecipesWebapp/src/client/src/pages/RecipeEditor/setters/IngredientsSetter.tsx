import React from 'react';

import { FaPlus, FaTrash } from 'react-icons/fa6';
import type { IngredientItemData } from '~/pages/RecipeEditor/models/IngredientItemData';
import { FormTextInput } from '~/sharedComponents/forms/FormTextInput';
import { FormLabel } from '~/sharedComponents/forms/FormLabel';
import { ingredientsAtom } from '../atoms/recipeDataAtom';
import { useAtom } from 'jotai';
import { Button } from '~/sharedComponents/Button';

export const IngredientsSetter: React.FC = () => {
  const [ingredients, setIngredients] = useAtom(ingredientsAtom);

  const addIngredient = () => {
    const newIngredient: IngredientItemData = {
      note: '',
    };

    const newIngredients = [...ingredients, newIngredient];

    setIngredients(newIngredients);
  };

  const removeIngredient = (indexToRemove: number) => {
    const newIngredients = ingredients.filter((_, index) => index !== indexToRemove);

    setIngredients(newIngredients);
  };

  const updateIngredient = (indexToUpdate: number, note: string) => {
    const newIngredients = ingredients.map((ingredient, index) =>
      index === indexToUpdate ? { ...ingredient, note } : ingredient,
    );

    setIngredients(newIngredients);
  };

  return (
    <>
      <FormLabel>Ingredients</FormLabel>

      <div className="mb-4">
        {ingredients.map((ingredient, index) => (
          <div key={index} className="flex flex-row items-center gap-2 mb-4">
            <div className="flex flex-col justify-center h-10">
              <span className="text-base">{index + 1}.</span>
            </div>

            <FormTextInput
              type="text"
              className="h-10"
              placeholder="e.g., 2 cups flour, 1 tsp salt..."
              value={ingredient.note}
              onChange={(e) => updateIngredient(index, e.target.value)}
            />

            <Button className="h-10" onClick={() => removeIngredient(index)}>
              <FaTrash size="0.875rem" />
            </Button>
          </div>
        ))}
      </div>

      <Button onClick={addIngredient}>
        <FaPlus className="inline-block align-middle mr-1" />
        <span className="align-middle">Add Ingredient</span>
      </Button>

      <div className="mt-2">
        <small>
          Include amounts and units (e.g., &quot;2 cups flour&quot;, &quot;1 tsp vanilla
          extract&quot;)
        </small>
      </div>
    </>
  );
};
