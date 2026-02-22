import { useMutation } from '@tanstack/react-query';
import { useAbortSignal } from '~/abort/useAbortSignal';
import { usersService } from '~/api/users/usersService';

export const useLogOutUserMutation = () => {
  const { createAbortSignal, finishAbortSignal } = useAbortSignal();

  return useMutation({
    mutationFn: async () => {
      const signal = createAbortSignal();

      await usersService.logOutUser(signal);
    },
    onMutate: () => {
      finishAbortSignal();
    },
    throwOnError: true,
  });
};
