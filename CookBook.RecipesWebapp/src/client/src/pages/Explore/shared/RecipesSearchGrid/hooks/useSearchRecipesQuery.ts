import { useQuery } from '@tanstack/react-query';
import { recipesService } from '~/api/recipes/recipesService';

export const useSearchRecipesQuery = (searchTerm: string) => {
  return useQuery({
    queryKey: ['searchRecipes', searchTerm],
    queryFn: async ({ signal }) => {
      const { status, data } = await recipesService.searchRecipes(searchTerm, signal);

      if (status === 204) {
        return {
          recipes: [],
        };
      }

      return data;
    },
    retry: 0,
    gcTime: 0,
    refetchOnWindowFocus: false,
  });
};
