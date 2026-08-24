import type { ValidationResult } from '~/forms/ValidationResult';

export const validateDisplayName = (displayName: string): ValidationResult => {
  if (displayName.trim().length < 3 || displayName.length > 100) {
    return {
      isValid: false,
      errorMessage: 'The display name must be in between 3 and 100 characters.',
    };
  }

  return { isValid: true };
};
