import { useMutation } from '@tanstack/react-query';

import { recipesService } from '~/api/recipes/recipesService';
import { useLoggedUser } from '~/authentication/LoggedUserProvider';

export const useRemoveRecipeMutation = (recipeId: number) => {
  const { user } = useLoggedUser();

  return useMutation({
    mutationFn: async () => {
      await recipesService.removeRecipe(recipeId, user.userId);
    },
  });
};
