import type { ValidationResult } from '~/models/forms/ValidationResult';
import type { IngredientItemData } from '../models/IngredientItemData';

export const validateRecipeIngredient = (
  recipeIngredient: IngredientItemData,
): ValidationResult => {
  if (recipeIngredient === undefined) {
    return { isValid: true };
  }

  if (recipeIngredient.note.length > 256) {
    return {
      isValid: false,
      errorMessage: 'The recipe ingredient note must not exceed 256 characters.',
    };
  }

  return { isValid: true };
};

export const validateRecipeIngredients = (
  recipeIngredients: IngredientItemData[],
): ValidationResult => {
  for (const recipeIngredint of recipeIngredients) {
    const validationResult = validateRecipeIngredient(recipeIngredint);

    if (!validationResult.isValid) {
      return validationResult;
    }
  }

  return {
    isValid: true,
  };
};
