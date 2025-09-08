import type { ValidationResult } from '~/models/forms/ValidationResult';
import type { InstructionItemData } from '../models/InstructionItemData';

export const validateRecipeInstruction = (
  recipeInstruction: InstructionItemData,
): ValidationResult => {
  if (recipeInstruction === undefined) {
    return { isValid: true };
  }

  if (recipeInstruction.note.length > 1024) {
    return {
      isValid: false,
      errorMessage: 'The recipe instruction note must not exceed 1024 characters.',
    };
  }

  return { isValid: true };
};

export const validateRecipeInstructions = (
  recipeInstructions: InstructionItemData[],
): ValidationResult => {
  for (const recipeInstruction of recipeInstructions) {
    const validationResult = validateRecipeInstruction(recipeInstruction);

    if (!validationResult.isValid) {
      return validationResult;
    }
  }

  return {
    isValid: true,
  };
};
