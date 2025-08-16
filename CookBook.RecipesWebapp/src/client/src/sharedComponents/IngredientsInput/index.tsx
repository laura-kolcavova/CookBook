import React, { useState } from 'react';
import { Input, InputGroup, InputGroupText } from 'reactstrap';
import { IngredientsContainer, IngredientItem, AddButton, RemoveButton } from './styled';
import { FaPlus, FaTrash } from 'react-icons/fa6';

export interface Ingredient {
  localId?: number;
  note: string;
}

interface IngredientsInputProps {
  value: Ingredient[];
  onChange: (ingredients: Ingredient[]) => void;
  label: string;
}

export const IngredientsInput: React.FC<IngredientsInputProps> = ({ value, onChange, label }) => {
  const [nextId, setNextId] = useState(Math.max(0, ...value.map((ing) => ing.localId || 0)) + 1);

  const addIngredient = () => {
    const newIngredient: Ingredient = {
      localId: nextId,
      note: '',
    };
    onChange([...value, newIngredient]);
    setNextId(nextId + 1);
  };

  const removeIngredient = (localId: number) => {
    onChange(value.filter((ing) => ing.localId !== localId));
  };

  const updateIngredient = (localId: number, note: string) => {
    onChange(value.map((ing) => (ing.localId === localId ? { ...ing, note } : ing)));
  };

  return (
    <IngredientsContainer>
      <label>{label}</label>

      {value.map((ingredient, index) => (
        <IngredientItem key={ingredient.localId}>
          <InputGroup>
            <InputGroupText>{index + 1}.</InputGroupText>
            <Input
              type="text"
              placeholder="e.g., 2 cups flour, 1 tsp salt..."
              value={ingredient.note}
              onChange={(e) => updateIngredient(ingredient.localId!, e.target.value)}
            />
            <RemoveButton
              type="button"
              color="outline-danger"
              onClick={() => removeIngredient(ingredient.localId!)}
              disabled={value.length === 1}>
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
