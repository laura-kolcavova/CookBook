import { useMutation } from '@tanstack/react-query';

import { recipeDataAtom } from '../atoms/recipeDataAtom';
import { useAtomValue } from 'jotai';
import { recipesService } from '~/api/recipes/recipesService';
import type { SaveRecipeRequestDto } from '~/api/recipes/dto/SaveRecipeRequestDto';
import { useLoggedUser } from '~/authentication/LoggedUserProvider';
import { useAbortSignal } from '~/abort/useAbortSignal';

export const useSaveRecipeMutation = () => {
  const { user } = useLoggedUser();

  const { createAbortSignal, finishAbortSignal } = useAbortSignal();

  const recipeData = useAtomValue(recipeDataAtom);

  return useMutation({
    mutationFn: async () => {
      const signal = createAbortSignal();

      const saveRecipeRequest: SaveRecipeRequestDto = {
        recipeId: recipeData.recipeId,
        userId: user.userId,
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
    onMutate: () => {
      finishAbortSignal();
    },
  });
};
