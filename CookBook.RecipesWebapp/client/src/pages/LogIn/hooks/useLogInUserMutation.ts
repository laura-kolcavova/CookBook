import { useMutation } from '@tanstack/react-query';
import { useAbortSignal } from '~/abort/useAbortSignal';
import type { LogInUserRequestDto } from '~/api/users/dto/LogInUserRequestDto';
import { usersService } from '~/api/users/usersService';

export const useLogInUserMutation = () => {
  const { createAbortSignal, finishAbortSignal } = useAbortSignal();

  return useMutation({
    mutationFn: async (logInUserRequest: LogInUserRequestDto) => {
      const signal = createAbortSignal();

      const { data } = await usersService.logInUser(logInUserRequest, signal);

      return data;
    },
    onMutate: () => {
      finishAbortSignal();
    },
  });
};
