import { useQuery, type UseQueryResult } from '@tanstack/react-query';
import type { GetRecipeDetailResponseDto } from '~/apiStores/recipes/models/GetRecipeDetailResponseDto';
import RecipesService from '~/apiStores/recipes/recipesService';
import type { AxiosGenericError } from '~/models/errors/AxiosGenericError';

export const useRecipeDetailQuery = (
  recipeId: number,
): UseQueryResult<GetRecipeDetailResponseDto | null, Error | AxiosGenericError> => {
  return useQuery({
    queryKey: ['recipeDetail', recipeId],
    queryFn: async ({ signal }) => {
      const { status, data } = await RecipesService.getRecipeDetail(recipeId, signal);

      if (status === 204) {
        return null;
      }

      return data;
    },
    retry: 0,
    refetchOnWindowFocus: false,
  });
};
