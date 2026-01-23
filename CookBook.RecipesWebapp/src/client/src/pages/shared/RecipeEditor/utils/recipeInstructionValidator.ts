import type { ValidationResult } from '~/forms/ValidationResult';
import type { RecipeInstructionData } from '../models/RecipeInstructionData';

export const validateRecipeInstruction = (
  recipeInstruction: RecipeInstructionData,
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
  recipeInstructions: RecipeInstructionData[],
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
