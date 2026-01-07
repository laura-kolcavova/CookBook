import { ValidationResult } from '~/forms/ValidationResult';

export const validateRecipeCookTime = (recipeCookTime: number): ValidationResult => {
  if (recipeCookTime < 0) {
    return {
      isValid: false,
      errorMessage: 'The recipe cook time must not be less than 0.',
    };
  }

  return { isValid: true };
};
