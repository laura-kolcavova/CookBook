import type { ValidationResult } from '~/forms/ValidationResult';
import type { RecipeIngredientData } from '../models/RecipeIngredientData';

export const validateRecipeIngredient = (
  recipeIngredient: RecipeIngredientData,
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
  recipeIngredients: RecipeIngredientData[],
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
