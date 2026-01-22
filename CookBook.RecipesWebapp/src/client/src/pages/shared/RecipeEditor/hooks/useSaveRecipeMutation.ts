import { useMutation } from '@tanstack/react-query';

import { recipeDataAtom } from '../atoms/recipeDataAtom';
import { useAtomValue } from 'jotai';
import { recipesService } from '~/api/recipes/recipesService';
import type { SaveRecipeRequestDto } from '~/api/recipes/dto/SaveRecipeRequestDto';
import { useLoggedUser } from '~/authentication/LoggedUserProvider';

export const useSaveRecipeMutation = () => {
  const { user } = useLoggedUser();

  const recipeData = useAtomValue(recipeDataAtom);

  return useMutation({
    mutationFn: async () => {
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

      const { data } = await recipesService.saveRecipe(saveRecipeRequest);

      return data;
    },
  });
};
