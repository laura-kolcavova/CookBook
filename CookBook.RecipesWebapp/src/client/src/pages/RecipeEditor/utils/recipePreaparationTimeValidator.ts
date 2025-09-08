import type { ValidationResult } from '~/models/forms/ValidationResult';

export const validateRecipePreparationTime = (recipePreparationTime: number): ValidationResult => {
  if (recipePreparationTime < 0) {
    return {
      isValid: false,
      errorMessage: 'The recipe preparation time must not be less than 0.',
    };
  }

  return { isValid: true };
};
