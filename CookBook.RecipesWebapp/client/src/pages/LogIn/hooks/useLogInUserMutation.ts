import { useMutation } from '@tanstack/react-query';
import { useAbortSignal } from '~/abort/useAbortSignal';
import { usersService } from '~/api/users/usersService';
import { pages } from '~/navigation/pages';

export const useLogInUserMutation = () => {
  const { createAbortSignal, finishAbortSignal } = useAbortSignal();

  return useMutation({
    mutationFn: async () => {
      const signal = createAbortSignal();

      await usersService.logInUser(pages.Home.paths[0], signal);
    },
    onMutate: () => {
      finishAbortSignal();
    },
  });
};
