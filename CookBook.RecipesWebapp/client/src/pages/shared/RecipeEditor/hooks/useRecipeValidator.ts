import { useAtomValue } from 'jotai';
import { useCallback } from 'react';
import {
  titleAtom,
  descriptionAtom,
  servingsAtom,
  cookTimeAtom,
  notesAtom,
  ingredientsAtom,
  instructionsAtom,
} from '../atoms/recipeDataAtom';

import { validateRecipeCookTime } from '../utils/recipeCookTimeValidator';
import { validateRecipeDescription } from '../utils/recipeDescriptionValidator';
import { validateRecipeIngredients } from '../utils/recipeIngredientValidator';
import { validateRecipeInstructions } from '../utils/recipeInstructionValidator';
import { validateRecipeNotes } from '../utils/recipeNotesValidator';
import { validateRecipeServings } from '../utils/recipeServingsValidator';
import { validateRecipeTitle } from '../utils/recipeTitleValidator';
import type { FieldValidations } from '~/forms/FieldValidations';

export const useRecipeValidator = () => {
  const title = useAtomValue(titleAtom);
  const description = useAtomValue(descriptionAtom);
  const servings = useAtomValue(servingsAtom);
  const cookTime = useAtomValue(cookTimeAtom);
  const notes = useAtomValue(notesAtom);
  const ingredients = useAtomValue(ingredientsAtom);
  const instructions = useAtomValue(instructionsAtom);

  const validate = useCallback(() => {
    const validations: FieldValidations = {
      title: validateRecipeTitle(title),
      description: validateRecipeDescription(description),
      servings: validateRecipeServings(servings),
      cookTime: validateRecipeCookTime(cookTime),
      notes: validateRecipeNotes(notes),
      ingredients: validateRecipeIngredients(ingredients),
      instructions: validateRecipeInstructions(instructions),
    };

    return validations;
  }, [cookTime, description, ingredients, instructions, notes, servings, title]);

  return {
    validate,
  };
};
