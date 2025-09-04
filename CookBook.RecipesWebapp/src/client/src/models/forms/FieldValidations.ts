import { ValidationResult } from './ValidationResult';

export type FieldValidations = {
  [key in string]: ValidationResult;
};
