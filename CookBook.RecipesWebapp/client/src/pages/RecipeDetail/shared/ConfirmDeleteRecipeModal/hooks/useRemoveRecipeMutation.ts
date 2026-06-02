import { useMutation } from '@tanstack/react-query';
import { useNavigate } from 'react-router-dom';
import { useAbortSignal } from '~/abort/useAbortSignal';

import { recipesService } from '~/api/recipes/recipesService';
import { useCurrentUser } from '~/authentication/CurrentUserProvider';
import { useModals } from '~/modals/ModalProvider';
import { pages } from '~/navigation/pages';

export const useRemoveRecipeMutation = (recipeId: number) => {
  const navigate = useNavigate();

  const { currentUser } = useCurrentUser();

  const { hideModal } = useModals();

  const { createAbortSignal, finishAbortSignal } = useAbortSignal();

  const redirectToHome = () => {
    navigate(pages.Home.paths[0]);
  };

  return useMutation({
    mutationFn: async () => {
      const signal = createAbortSignal();

      await recipesService.removeRecipe(recipeId, currentUser.userName, signal);
    },
    onSuccess: () => {
      hideModal();
      redirectToHome();
    },
    onMutate: () => {
      finishAbortSignal();
    },
  });
};
