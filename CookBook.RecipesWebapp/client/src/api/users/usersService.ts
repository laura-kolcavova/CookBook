import type { AxiosPromise, GenericAbortSignal } from 'axios';
import { callAxios } from '~/utils/axios';
import type { RegisterUserRequestDto } from './dto/RegisterUserRequestDto';
import type { LogInUserRequestDto } from './dto/LogInUserRequestDto';

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

const logInUser = (
  logInUserRequest: LogInUserRequestDto,
  signal?: GenericAbortSignal,
): AxiosPromise<void> => {
  return callAxios({
    url: `/api/users/login`,
    method: 'POST',
    data: logInUserRequest,
    signal: signal,
  });
};

export const usersService = {
  registerUser,
  logInUser,
};
