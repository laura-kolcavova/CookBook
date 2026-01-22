import type { ValidationResult } from '~/forms/ValidationResult';

export const validateRecipeServings = (recipeServings: number): ValidationResult => {
  if (recipeServings < 0) {
    return {
      isValid: false,
      errorMessage: 'The recipe servings must not be less than 0.',
    };
  }

  return { isValid: true };
};
