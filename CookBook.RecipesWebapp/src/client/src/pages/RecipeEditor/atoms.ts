import { atomWithReset } from 'jotai/utils';
import { EMPTY_RECIPE_DATA, RecipeData } from './models';
import { focusAtom } from 'jotai-optics';

const recipeDataAtom = atomWithReset<RecipeData>(EMPTY_RECIPE_DATA);

export const recipeTitleAtom = focusAtom(recipeDataAtom, (optic) => optic.prop('title'));

export const recipeDescriptionAtom = focusAtom(recipeDataAtom, (optic) =>
  optic.prop('description'),
);

export const recipeNotesAtom = focusAtom(recipeDataAtom, (optic) => optic.prop('notes'));
