import { useAtomValue } from 'jotai';
import { useCallback, useState } from 'react';
import { titleAtom, ingredientsAtom, instructionsAtom } from '../atoms/recipeDataAtom';
import { FieldValidation } from '~/models/forms/FieldValidation';

const validateTitle = (title: string): true | string => {
  if (title.length < 3 || title.length > 256) {
    return 'The recipe title must be in between 3 and 256 characters';
  }
  return true;
};

const validateIngredients = (ingredients: { note: string }[]): true | string => {
  if (ingredients.length === 0) {
    return 'At least one ingredient is required';
  }
  const hasValidIngredients = ingredients.some((ing) => ing.note.trim().length > 0);
  if (!hasValidIngredients) {
    return 'Please add at least one ingredient';
  }
  return true;
};

const validateInstructions = (instructions: { note: string }[]): true | string => {
  if (instructions.length === 0) {
    return 'At least one instruction step is required';
  }
  const hasValidInstructions = instructions.some((inst) => inst.note.trim().length > 0);
  if (!hasValidInstructions) {
    return 'Please add at least one instruction step';
  }
  return true;
};

type FieldValidations = {
  [key in string]: FieldValidation;
};

export const useRecipeValidator = () => {
  const title = useAtomValue(titleAtom);
  const ingredients = useAtomValue(ingredientsAtom);
  const instructions = useAtomValue(instructionsAtom);

  const [validations, setValidations] = useState<FieldValidations>({});

  const validate = useCallback(() => {
    let isValid = true;

    const newValidations: FieldValidations = {
      title: { isValid: true },
      ingredients: { isValid: true },
      instructions: { isValid: true },
    };

    // Validate title
    const titleValidationResult = validateTitle(title);
    if (titleValidationResult !== true) {
      newValidations.title = {
        isValid: false,
        invalidMessage: titleValidationResult,
      };
      isValid = false;
    }

    // Validate ingredients
    const ingredientsValidationResult = validateIngredients(ingredients);
    if (ingredientsValidationResult !== true) {
      newValidations.ingredients = {
        isValid: false,
        invalidMessage: ingredientsValidationResult,
      };
      isValid = false;
    }

    // Validate instructions
    const instructionsValidationResult = validateInstructions(instructions);
    if (instructionsValidationResult !== true) {
      newValidations.instructions = {
        isValid: false,
        invalidMessage: instructionsValidationResult,
      };
      isValid = false;
    }

    setValidations(newValidations);
    return isValid;
  }, [title, ingredients, instructions]);

  const resetValidations = useCallback(() => {
    setValidations({});
  }, []);

  return {
    validate,
    resetValidations,
    validations,
  };
};
