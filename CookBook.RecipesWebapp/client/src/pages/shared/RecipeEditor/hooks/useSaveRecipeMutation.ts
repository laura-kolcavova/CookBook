import { useMutation } from '@tanstack/react-query';

import { useAtomValue } from 'jotai';
import { useNavigate } from 'react-router-dom';
import { recipeDataAtom } from '../atoms/recipeDataAtom';
import { useRecipeData } from './useRecipeData';
import { useAbortSignal } from '~/abort/useAbortSignal';
import type { SaveRecipeRequestDto } from '~/api/recipes/dto/SaveRecipeRequestDto';
import { recipesService } from '~/api/recipes/recipesService';
import { pages } from '~/navigation/pages';

export const useSaveRecipeMutation = () => {
  const { createSignal, finishSignal } = useAbortSignal();

  const navigate = useNavigate();

  const { resetData } = useRecipeData();

  const recipeData = useAtomValue(recipeDataAtom);

  return useMutation({
    mutationFn: async () => {
      const signal = createSignal();

      const saveRecipeRequest: SaveRecipeRequestDto = {
        recipeId: recipeData.recipeId,
        title: recipeData.title,
        description: recipeData.description,
        servings: recipeData.servings,
        cookTime: recipeData.cookTime,
        notes: recipeData.notes,
        ingredients: recipeData.ingredients.map((ingredient) => ({
          localId: ingredient.localId,
          note: ingredient.note,
        })),
        instructions: recipeData.instructions.map((instruction) => ({
          localId: instruction.localId,
          note: instruction.note,
        })),
        tags: [...recipeData.tags],
      };

      const { data } = await recipesService.saveRecipe(saveRecipeRequest, signal);

      return data;
    },
    onSuccess: (data) => {
      resetData();

      const recipeDetailPath = pages.RecipeDetail.paths[0].replace(
        ':recipeId',
        data.recipeId.toString(),
      );

      navigate(recipeDetailPath);
    },
    onMutate: () => {
      finishSignal();
    },
  });
};
