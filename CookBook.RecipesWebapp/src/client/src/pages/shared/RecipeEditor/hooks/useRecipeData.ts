import { useAtomValue, useSetAtom } from 'jotai';
import { useCallback } from 'react';
import type { RecipeDetailDto } from '~/api/recipes/dto/RecipeDetailDto';
import { recipeDataAtom } from '../atoms/recipeDataAtom';
import type { RecipeData } from '../models/RecipeData';
import type { RecipeIngredientData } from '../models/RecipeIngredientData';
import type { RecipeInstructionData } from '../models/RecipeInstructionData';
import { atomWithReset, useResetAtom } from 'jotai/utils';

const dataInitializedFromRecipeAtom = atomWithReset<boolean>(false);

export const useRecipeData = () => {
  const dataInitializedFromRecipe = useAtomValue(dataInitializedFromRecipeAtom);
  const setDataInitializedFromRecipe = useSetAtom(dataInitializedFromRecipeAtom);
  const resetDataInitializedFromRecipe = useResetAtom(dataInitializedFromRecipeAtom);

  const setRecipeData = useSetAtom(recipeDataAtom);
  const resetRecipeData = useResetAtom(recipeDataAtom);

  const initializeDataFromRecipe = useCallback(
    (recipe: RecipeDetailDto) => {
      const recipeData: RecipeData = {
        recipeId: recipe.recipeId,
        title: recipe.title,
        description: recipe.description,
        servings: recipe.servings,
        cookTime: recipe.cookTime,
        notes: recipe.notes,
        ingredients: recipe.ingredients.map<RecipeIngredientData>((ingredient) => ({
          localId: ingredient.localId,
          note: ingredient.note,
        })),
        instructions: recipe.instructions.map<RecipeInstructionData>((instruction) => ({
          localId: instruction.localId,
          note: instruction.note,
        })),
        tags: [...recipe.tags],
      };

      setRecipeData(recipeData);
      setDataInitializedFromRecipe(true);
    },
    [setRecipeData, setDataInitializedFromRecipe],
  );

  const resetData = useCallback(() => {
    resetRecipeData();
    resetDataInitializedFromRecipe();
  }, [resetRecipeData, resetDataInitializedFromRecipe]);

  return { initializeDataFromRecipe, resetData, dataInitializedFromRecipe };
};
