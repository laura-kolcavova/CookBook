import { useQuery } from '@tanstack/react-query';
import { recipesService } from '~/api/recipes/recipesService';

export const useRecipeDetailQuery = (recipeId: number) => {
  return useQuery({
    queryKey: ['recipeDetail', recipeId],
    queryFn: async ({ signal }) => {
      const { status, data } = await recipesService.getRecipeDetail(recipeId, signal);

      if (status === 204) {
        return null;
      }

      return data;
    },
    retry: 0,
    gcTime: 0,
    refetchOnWindowFocus: false,
  });
};
