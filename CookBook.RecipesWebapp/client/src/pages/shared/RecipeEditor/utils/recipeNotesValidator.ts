import type { ValidationResult } from '~/forms/ValidationResult';

export const validateRecipeNotes = (recipeNotes: string | null): ValidationResult => {
  if (recipeNotes === null) {
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
