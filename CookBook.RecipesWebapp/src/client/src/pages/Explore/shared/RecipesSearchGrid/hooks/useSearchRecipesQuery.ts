import { useInfiniteQuery, useQueryClient } from '@tanstack/react-query';
import { useCallback } from 'react';
import { recipesService } from '~/api/recipes/recipesService';

const PAGE_SIZE = 20;

const getQueryKey = (searchTerm?: string) => {
  return ['searchRecipes', searchTerm];
};

export const useSearchRecipesQuery = (searchTerm?: string) => {
  const queryClient = useQueryClient();

  const query = useInfiniteQuery({
    queryKey: getQueryKey(searchTerm),
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

  const resetAndRefetch = useCallback(async () => {
    await queryClient.resetQueries({ queryKey: getQueryKey(searchTerm) });
    await query.refetch();
  }, []);

  return {
    ...query,
    resetAndRefetch,
  };
};
