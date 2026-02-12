import { useMutation } from '@tanstack/react-query';
import { useAbortSignal } from '~/abort/useAbortSignal';
import type { RegisterUserRequestDto } from '~/api/users/dto/RegisterUserRequestDto';
import { usersService } from '~/api/users/usersService';

export const useRegisterUserMutation = () => {
  const { createAbortSignal, finishAbortSignal } = useAbortSignal();

  return useMutation({
    mutationFn: async (registerUserRequest: RegisterUserRequestDto) => {
      const signal = createAbortSignal();

      const { data } = await usersService.registerUser(registerUserRequest, signal);

      return data;
    },
    onMutate: () => {
      finishAbortSignal();
    },
  });
};
