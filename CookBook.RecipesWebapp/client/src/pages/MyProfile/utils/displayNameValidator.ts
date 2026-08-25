import type { ValidationResult } from '~/forms/ValidationResult';

export const validateDisplayName = (displayName: string): ValidationResult => {
  if (displayName.length > 256) {
    return {
      isValid: false,
      errorMessage: 'The display name must be less than 256 characters.',
    };
  }

  return { isValid: true };
};
