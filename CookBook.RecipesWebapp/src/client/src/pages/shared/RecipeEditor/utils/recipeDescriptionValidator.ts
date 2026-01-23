import type { ValidationResult } from '~/forms/ValidationResult';

export const validateRecipeDescription = (recipeDescription: string | null): ValidationResult => {
  if (recipeDescription === null) {
    return { isValid: true };
  }

  console.log(recipeDescription);
  if (recipeDescription.length > 1024) {
    return {
      isValid: false,
      errorMessage: 'The recipe description must not exceed 1024 characters.',
    };
  }

  return { isValid: true };
};
