import { atomWithReset } from 'jotai/utils';
import { RecipeData } from '../models/RecipeData';
import { focusAtom } from 'jotai-optics';

export const EMPTY_RECIPE_DATA: RecipeData = {
  recipeId: undefined,
  title: '',
  description: null,
  servings: 0,
  preparationTime: 0,
  cookTime: 0,
  notes: undefined,
  ingredients: [],
  instructions: [],
  tags: [],
};

export const recipeDataAtom = atomWithReset<RecipeData>(EMPTY_RECIPE_DATA);

export const recipeTitleAtom = focusAtom(recipeDataAtom, (optic) => optic.prop('title'));

export const recipeDescriptionAtom = focusAtom(recipeDataAtom, (optic) =>
  optic.prop('description'),
);

export const recipeNotesAtom = focusAtom(recipeDataAtom, (optic) => optic.prop('notes'));
