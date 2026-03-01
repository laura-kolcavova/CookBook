import type { AxiosPromise, GenericAbortSignal } from 'axios';
import { callAxios } from '~/utils/axios';
import type { RegisterUserRequestDto } from './dto/RegisterUserRequestDto';
import type { CurrentUserDto } from './dto/CurrentUserDto';

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

const logInUser = (returnUrl?: string, signal?: GenericAbortSignal): AxiosPromise<void> => {
  return callAxios({
    url: `/api/users/login`,
    method: 'GET',
    params: {
      returnUrl,
    },
    signal: signal,
  });
};

const logOutUser = (signal?: GenericAbortSignal): AxiosPromise<void> => {
  return callAxios({
    url: `/api/users/logout`,
    method: 'POST',
    signal: signal,
  });
};

const getCurrentUser = (signal?: GenericAbortSignal): AxiosPromise<CurrentUserDto> => {
  return callAxios({
    url: `/api/users/current`,
    method: 'GET',
    signal: signal,
  });
};

export const usersService = {
  registerUser,
  logInUser,
  logOutUser,
  getCurrentUser,
};
