import { useMutation } from '@tanstack/react-query';
import { useNavigate } from 'react-router-dom';
import { useAbortSignal } from '~/abort/useAbortSignal';

import { recipesService } from '~/api/recipes/recipesService';
import { useModals } from '~/modals/ModalProvider';
import { pages } from '~/navigation/pages';

export const useRemoveRecipeMutation = (recipeId: number) => {
  const navigate = useNavigate();

  const { hideModal } = useModals();

  const { createSignal, finishSignal } = useAbortSignal();

  const redirectToHome = () => {
    navigate(pages.Home.paths[0]);
  };

  return useMutation({
    mutationFn: async () => {
      const signal = createSignal();

      await recipesService.removeRecipe(recipeId, signal);
    },
    onSuccess: () => {
      hideModal();
      redirectToHome();
    },
    onMutate: () => {
      finishSignal();
    },
  });
};
