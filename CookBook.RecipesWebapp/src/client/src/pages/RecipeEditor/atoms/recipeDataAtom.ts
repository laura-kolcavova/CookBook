import { atomWithReset } from 'jotai/utils';
import type { RecipeData } from '../models/RecipeData';
import { focusAtom } from 'jotai-optics';

export const EMPTY_RECIPE_DATA: RecipeData = {
  recipeId: undefined,
  title: '',
  description: undefined,
  servings: 0,
  preparationTime: 0,
  cookTime: 0,
  notes: undefined,
  ingredients: [],
  instructions: [],
  tags: [],
};

export const recipeDataAtom = atomWithReset<RecipeData>(EMPTY_RECIPE_DATA);

export const titleAtom = focusAtom(recipeDataAtom, (optic) => optic.prop('title'));

export const descriptionAtom = focusAtom(recipeDataAtom, (optic) => optic.prop('description'));

export const notesAtom = focusAtom(recipeDataAtom, (optic) => optic.prop('notes'));

export const servingsAtom = focusAtom(recipeDataAtom, (optic) => optic.prop('servings'));

export const preparationTimeAtom = focusAtom(recipeDataAtom, (optic) =>
  optic.prop('preparationTime'),
);

export const cookTimeAtom = focusAtom(recipeDataAtom, (optic) => optic.prop('cookTime'));

export const ingredientsAtom = focusAtom(recipeDataAtom, (optic) => optic.prop('ingredients'));

export const instructionsAtom = focusAtom(recipeDataAtom, (optic) => optic.prop('instructions'));

export const tagsAtom = focusAtom(recipeDataAtom, (optic) => optic.prop('tags'));
