import type { AxiosPromise, GenericAbortSignal } from 'axios';
import { callAxios } from '~/utils/axios';
import type { RegisterUserRequestDto } from './dto/RegisterUserRequestDto';

const registerUser = (
  registerUserRequest: RegisterUserRequestDto,
  signal?: GenericAbortSignal,
): AxiosPromise<void> => {
  return callAxios({
    url: `/api/users/register`,
    method: 'POST',
    data: registerUserRequest,
    signal: signal,
  });
};

export const usersService = {
  registerUser,
};
