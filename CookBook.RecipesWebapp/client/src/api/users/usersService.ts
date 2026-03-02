import type { AxiosPromise, GenericAbortSignal } from 'axios';
import { callAxios } from '~/utils/axios';
import type { CurrentUserDto } from './dto/CurrentUserDto';

const getCurrentUser = (signal?: GenericAbortSignal): AxiosPromise<CurrentUserDto> => {
  return callAxios({
    url: `/api/users/current`,
    method: 'GET',
    signal: signal,
  });
};

const getLogInUserUrl = (returnUrl?: string): string => {
  const basePath = '/api/users/login';

  if (returnUrl) {
    return `${basePath}?returnUrl=${encodeURIComponent(returnUrl)}`;
  }

  return basePath;
};

const logOutUser = (signal?: GenericAbortSignal): AxiosPromise<void> => {
  return callAxios({
    url: `/api/users/logout`,
    method: 'POST',
    signal: signal,
  });
};

export const usersService = {
  getCurrentUser,
  getLogInUserUrl,
  logOutUser,
};
