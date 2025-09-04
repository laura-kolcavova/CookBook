import { useAtomValue } from 'jotai';
import { useCallback } from 'react';
import {
  titleAtom,
  descriptionAtom,
  servingsAtom,
  preparationTimeAtom,
  cookTimeAtom,
  notesAtom,
  ingredientsAtom,
  instructionsAtom,
} from '../atoms/recipeDataAtom';

import { FieldValidations } from '~/models/forms/FieldValidations';
import { validateRecipeTitle } from '../utils/recipeTitleValidator';
import { validateRecipeDescription } from '../utils/recipeDescriptionValidator';
import { validateRecipeServings } from '../utils/recipeServingsValidator';
import { validateRecipePreparationTime } from '../utils/recipePreaparationTimeValidator';
import { validateRecipeCookTime } from '../utils/recipeCookTimeValidator';
import { validateRecipeNotes } from '../utils/recipeNotesValidator';
import { validateRecipeIngredients } from '../utils/recipeIngredientValidator';
import { validateRecipeInstructions } from '../utils/recipeInstructionValidator';

export const useRecipeValidator = () => {
  const title = useAtomValue(titleAtom);
  const description = useAtomValue(descriptionAtom);
  const servings = useAtomValue(servingsAtom);
  const preparationTime = useAtomValue(preparationTimeAtom);
  const cookTime = useAtomValue(cookTimeAtom);
  const notes = useAtomValue(notesAtom);
  const ingredients = useAtomValue(ingredientsAtom);
  const instructions = useAtomValue(instructionsAtom);

  const validate = useCallback(() => {
    const validations: FieldValidations = {
      title: validateRecipeTitle(title),
      description: validateRecipeDescription(description),
      servings: validateRecipeServings(servings),
      preparationTime: validateRecipePreparationTime(preparationTime),
      cookTime: validateRecipeCookTime(cookTime),
      notes: validateRecipeNotes(notes),
      ingredients: validateRecipeIngredients(ingredients),
      instructions: validateRecipeInstructions(instructions),
    };

    return validations;
  }, [cookTime, description, ingredients, instructions, notes, preparationTime, servings, title]);

  return {
    validate,
  };
};
