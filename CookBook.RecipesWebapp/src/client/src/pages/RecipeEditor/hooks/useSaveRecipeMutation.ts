import { useMutation } from '@tanstack/react-query';

import { recipeDataAtom } from '../atoms/recipeDataAtom';
import { useAtomValue } from 'jotai';

import RecipesService from '~/apiStores/recipes/recipesService';
import type { SaveRecipeResponseDto } from '~/apiStores/recipes/models/SaveRecipeResponseDto';
import type { AxiosGenericError } from '~/models/errors/AxiosGenericError';
import type { IngredientItemDto } from '~/apiStores/recipes/models/IngredientItemDto';
import type { InstructionItemDto } from '~/apiStores/recipes/models/InstructionItemDto';

export const useSaveRecipeMutation = () => {
  const recipeData = useAtomValue(recipeDataAtom);

  return useMutation<SaveRecipeResponseDto, Error | AxiosGenericError>({
    mutationFn: async () => {
      const { data } = await RecipesService.saveRecipe({
        recipeId: recipeData.recipeId,
        userId: 1,
        title: recipeData.title,
        descripiton: recipeData.description,
        servings: recipeData.servings,
        preparationTime: recipeData.preparationTime,
        cookTime: recipeData.cookTime,
        notes: recipeData.notes,
        ingredients: recipeData.ingredients.map<IngredientItemDto>((ingredient) => ({
          localId: ingredient.localId,
          note: ingredient.note,
        })),
        instructions: recipeData.instructions.map<InstructionItemDto>((instruction) => ({
          localId: instruction.localId,
          note: instruction.note,
        })),
        tags: [...recipeData.tags],
      });

      return data;
    },
  });
};
