import { useMutation } from '@tanstack/react-query';
import { useIntl } from 'react-intl';
import { toast } from 'react-toastify';
import { messages } from '../messages';
import { useAbortSignal } from '~/abort/useAbortSignal';
import { usersService } from '~/api/users/usersService';
import { useCurrentUser } from '~/authentication/CurrentUserProvider';
import { useModals } from '~/modals/ModalProvider';

export const useChangeDisplayNameMutation = () => {
  const { createSignal, finishSignal } = useAbortSignal();

  const { refreshCurrentUser } = useCurrentUser();

  const { hideModal } = useModals();

  const { formatMessage } = useIntl();

  return useMutation({
    mutationFn: async (displayName: string) => {
      const signal = createSignal();

      await usersService.changeDisplayName({ displayName }, signal);
    },
    onSuccess: () => {
      refreshCurrentUser();
      hideModal();
      toast.success(formatMessage(messages.changeSuccessMessage));
    },
    onMutate: () => {
      finishSignal();
    },
  });
};
