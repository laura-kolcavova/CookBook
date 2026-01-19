import { useQuery } from '@tanstack/react-query';
import { recipesService } from '~/api/recipes/recipesService';
import type { SearchRecipesResponseDto } from '~/api/recipes/dto/SearchRecipesResponseDto';

export const useSearchRecipesQuery = (searchTerm: string) => {
  return useQuery<SearchRecipesResponseDto>({
    queryKey: ['recipes', 'search', searchTerm],
    queryFn: async ({ signal }) => {
      const response = await recipesService.searchRecipes(searchTerm, signal);
      return response.data;
    },
    enabled: !!searchTerm && searchTerm.trim().length > 0,
  });
};
