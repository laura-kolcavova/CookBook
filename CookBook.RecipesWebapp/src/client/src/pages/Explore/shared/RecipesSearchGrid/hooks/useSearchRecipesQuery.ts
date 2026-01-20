import { useInfiniteQuery, useQueryClient } from '@tanstack/react-query';
import { recipesService } from '~/api/recipes/recipesService';

const PAGE_SIZE = 20;

export const useSearchRecipesQuery = (searchTerm?: string) => {
  const queryKey = ['searchRecipes', searchTerm];

  const queryClient = useQueryClient();

  const query = useInfiniteQuery({
    queryKey: queryKey,
    queryFn: async ({ signal, pageParam }) => {
      const { status, data } = await recipesService.searchRecipes(
        searchTerm,
        pageParam,
        PAGE_SIZE,
        signal,
      );

      if (status === 204) {
        return {
          recipes: [],
        };
      }

      return data;
    },
    initialPageParam: 0,
    getNextPageParam: (lastPage, allPages) => {
      if (lastPage.recipes.length < PAGE_SIZE) {
        return undefined;
      }
      return allPages.length * PAGE_SIZE;
    },
    retry: 0,
    gcTime: 0,
    refetchOnWindowFocus: false,
  });

  const resetAndRefetch = async () => {
    await queryClient.resetQueries({ queryKey: queryKey });
    await query.refetch();
  };

  return {
    ...query,
    resetAndRefetch,
  };
};
