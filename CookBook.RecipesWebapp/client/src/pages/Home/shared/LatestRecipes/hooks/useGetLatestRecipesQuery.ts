import { useQuery } from '@tanstack/react-query';
import { recipesService } from '~/api/recipes/recipesService';

const latestRecipesFetchCount = 4;

export const useLatestRecipesQuery = () => {
  return useQuery({
    queryKey: ['getLatestRecipes'],
    queryFn: async ({ signal }) => {
      const { status, data } = await recipesService.getLatestRecipes(
        latestRecipesFetchCount,
        signal,
      );

      if (status === 204) {
        return {
          latestRecipes: [],
        };
      }

      return data;
    },
    retry: 0,
    gcTime: 0,
    refetchOnWindowFocus: false,
  });
};
