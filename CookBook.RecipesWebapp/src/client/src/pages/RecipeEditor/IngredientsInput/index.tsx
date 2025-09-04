import React from 'react';
import { Input, InputGroup, InputGroupText } from 'reactstrap';
import { IngredientsContainer, IngredientItem, AddButton, RemoveButton } from './styled';
import { FaPlus, FaTrash } from 'react-icons/fa6';
import { IngredientItemData } from '~/pages/RecipeEditor/models/IngredientItemData';

interface IngredientsInputProps {
  ingredients: IngredientItemData[];
  onChange: (newIngredients: IngredientItemData[]) => void;
  label: string;
}

export const IngredientsInput: React.FC<IngredientsInputProps> = ({
  ingredients,
  onChange,
  label,
}) => {
  const addIngredient = () => {
    const newIngredient: IngredientItemData = {
      note: '',
    };

    const newIngredients = [...ingredients, newIngredient];

    onChange(newIngredients);
  };

  const removeIngredient = (indexToRemove: number) => {
    const newIngredients = ingredients.filter((_, index) => index !== indexToRemove);

    onChange(newIngredients);
  };

  const updateIngredient = (indexToUpdate: number, note: string) => {
    const newIngredients = ingredients.map((ingredient, index) =>
      index === indexToUpdate ? { ...ingredient, note } : ingredient,
    );

    onChange(newIngredients);
  };

  return (
    <IngredientsContainer>
      <label>{label}</label>

      {ingredients.map((ingredient, index) => (
        <IngredientItem key={index}>
          <InputGroup>
            <InputGroupText>{index + 1}.</InputGroupText>
            <Input
              type="text"
              placeholder="e.g., 2 cups flour, 1 tsp salt..."
              value={ingredient.note}
              onChange={(e) => updateIngredient(index, e.target.value)}
            />
            <RemoveButton
              type="button"
              color="outline-danger"
              onClick={() => removeIngredient(index)}>
              <FaTrash />
            </RemoveButton>
          </InputGroup>
        </IngredientItem>
      ))}

      <AddButton type="button" color="outline-primary" onClick={addIngredient}>
        <FaPlus className="me-1" />
        Add Ingredient
      </AddButton>

      <small className="text-muted mt-1">
        Include amounts and units (e.g., &quot;2 cups flour&quot;, &quot;1 tsp vanilla
        extract&quot;)
      </small>
    </IngredientsContainer>
  );
};
