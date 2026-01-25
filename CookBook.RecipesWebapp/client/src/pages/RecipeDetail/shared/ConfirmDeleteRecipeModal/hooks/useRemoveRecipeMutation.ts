import { useMutation } from '@tanstack/react-query';
import { useAbortSignal } from '~/abort/useAbortSignal';

import { recipesService } from '~/api/recipes/recipesService';
import { useLoggedUser } from '~/authentication/LoggedUserProvider';

export const useRemoveRecipeMutation = (recipeId: number) => {
  const { user } = useLoggedUser();

  const { createAbortSignal, finishAbortSignal } = useAbortSignal();

  return useMutation({
    mutationFn: async () => {
      const signal = createAbortSignal();

      await recipesService.removeRecipe(recipeId, user.userId, signal);
    },
    onMutate: () => {
      finishAbortSignal();
    },
  });
};
