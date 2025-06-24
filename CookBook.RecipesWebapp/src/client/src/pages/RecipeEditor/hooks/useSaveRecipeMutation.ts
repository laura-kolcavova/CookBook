import { useMutation } from '@tanstack/react-query';

import { recipeDataAtom } from '../atoms/recipeDataAtom';
import { useAtomValue } from 'jotai';
import { IngredientItemDto } from '~/apiStores/recipes/models/IngredientItemDto';
import { InstructionItemDto } from '~/apiStores/recipes/models/InstructionItemDto';
import RecipesService from '~/apiStores/recipes/recipesService';
import { SaveRecipeResponseDto } from '~/apiStores/recipes/models/SaveRecipeResponseDto';
import { AxiosGenericError } from '~/models/errors/AxiosGenericError';

export const useSaveRecipeMutation = () => {
  // const { user } = useContext(UserIdentityContext);

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
