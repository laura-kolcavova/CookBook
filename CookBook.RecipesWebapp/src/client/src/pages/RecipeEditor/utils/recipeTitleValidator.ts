import type { ValidationResult } from '~/models/forms/ValidationResult';

export const validateRecipeTitle = (recipeTitle: string): ValidationResult => {
  if (recipeTitle.length < 3 || recipeTitle.length > 256) {
    return {
      isValid: false,
      errorMessage: 'The recipe title must be in between 3 and 256 characters.',
    };
  }

  return { isValid: true };
};
