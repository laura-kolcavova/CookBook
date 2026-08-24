import { useMutation } from '@tanstack/react-query';
import { useAbortSignal } from '~/abort/useAbortSignal';
import { usersService } from '~/api/users/usersService';
import { useCurrentUser } from '~/authentication/CurrentUserProvider';
import { useModals } from '~/modals/ModalProvider';

export const useUpdateDisplayNameMutation = () => {
  const { createSignal, finishSignal } = useAbortSignal();

  const { refreshCurrentUser } = useCurrentUser();

  const { hideModal } = useModals();

  return useMutation({
    mutationFn: async (displayName: string) => {
      const signal = createSignal();

      await usersService.updateDisplayName({ displayName }, signal);
    },
    onSuccess: () => {
      refreshCurrentUser();
      hideModal();
    },
    onMutate: () => {
      finishSignal();
    },
  });
};
