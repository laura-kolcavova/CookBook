import type { FieldValidations } from '~/models/forms/FieldValidations';

export const areValid = (validations: FieldValidations): boolean => {
  const validationResults = Object.values(validations);

  if (validationResults.length === 0) {
    return true;
  }

  return validationResults.every((validationResult) => validationResult.isValid);
};
