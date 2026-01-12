import { useMutation } from '@tanstack/react-query';

import { recipeDataAtom } from '../atoms/recipeDataAtom';
import { useAtomValue } from 'jotai';
import { SaveRecipeResponseDto } from '~/api/recipes/dto/SaveRecipeResponseDto';
import { AxiosGenericError } from '~/errors/AxiosGenericError';
import RecipesService from '~/api/recipes/recipesService';
import { IngredientItemDto } from '~/api/recipes/dto/IngredientItemDto';
import { InstructionItemDto } from '~/api/recipes/dto/InstructionItemDto';

export const useSaveRecipeMutation = () => {
  const recipeData = useAtomValue(recipeDataAtom);

  return useMutation<SaveRecipeResponseDto, Error | AxiosGenericError>({
    mutationFn: async () => {
      const { data } = await RecipesService.saveRecipe({
        recipeId: recipeData.recipeId,
        userId: 1,
        title: recipeData.title,
        description: recipeData.description,
        servings: recipeData.servings,
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
