import { useMutation } from '@tanstack/react-query';
import { useAbortSignal } from '~/abort/useAbortSignal';

import { recipesService } from '~/api/recipes/recipesService';
import { useCurrentUser } from '~/authentication/CurrentUserProvider';

export const useRemoveRecipeMutation = (recipeId: number) => {
  const { currentUser } = useCurrentUser();

  const { createAbortSignal, finishAbortSignal } = useAbortSignal();

  return useMutation({
    mutationFn: async () => {
      const signal = createAbortSignal();

      await recipesService.removeRecipe(recipeId, currentUser.userName, signal);
    },
    onMutate: () => {
      finishAbortSignal();
    },
  });
};
