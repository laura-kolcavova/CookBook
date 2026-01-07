import { FieldValidations } from '~/forms/FieldValidations';

export const areValid = (validations: FieldValidations): boolean => {
  const validationResults = Object.values(validations);

  if (validationResults.length === 0) {
    return true;
  }

  return validationResults.every((validationResult) => validationResult.isValid);
};
