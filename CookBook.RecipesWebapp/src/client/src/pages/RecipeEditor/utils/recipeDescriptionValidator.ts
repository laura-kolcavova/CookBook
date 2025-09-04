import { ValidationResult } from '~/models/forms/ValidationResult';

export const validateRecipeDescription = (recipeDescription?: string): ValidationResult => {
  if (recipeDescription === undefined) {
    return { isValid: true };
  }

  if (recipeDescription.length > 1024) {
    return {
      isValid: false,
      errorMessage: 'The recipe description must not exceed 1024 characters.',
    };
  }

  return { isValid: true };
};
