import { useState } from 'react';
import { useQuery } from '@tanstack/react-query';
import { recipesService } from '~/api/recipes/recipesService';

const initialLimit = 20;

export const useSearchRecipesQuery = (searchTerm?: string) => {
  const [limit, setLimit] = useState(initialLimit);

  const query = useQuery({
    queryKey: ['searchRecipes', searchTerm, limit],
    queryFn: async ({ signal }) => {
      const { status, data } = await recipesService.searchRecipes(searchTerm, 0, limit, signal);

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

  const loadMore = () => {
    setLimit((prev) => prev + initialLimit);
  };

  return {
    ...query,
    loadMore,
    hasMore: query.data ? query.data.recipes.length >= limit : false,
  };
};
