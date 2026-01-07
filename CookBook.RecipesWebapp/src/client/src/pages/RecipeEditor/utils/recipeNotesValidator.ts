import type { ValidationResult } from '~/models/forms/ValidationResult';

export const validateRecipeNotes = (recipeNotes?: string): ValidationResult => {
  if (recipeNotes === undefined) {
    return { isValid: true };
  }

  if (recipeNotes.length > 1024) {
    return {
      isValid: false,
      errorMessage: 'The recipe notes must not exceed 1024 characters.',
    };
  }

  return { isValid: true };
};
