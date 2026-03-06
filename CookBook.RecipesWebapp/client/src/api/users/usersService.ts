import type { AxiosPromise, GenericAbortSignal } from 'axios';
import { callAxios } from '~/utils/axios';
import type { CurrentUserDto } from './dto/CurrentUserDto';

const getLogInUserUrl = (returnUrl?: string): string => {
  const basePath = '/api/users/login';

  if (returnUrl) {
    return `${basePath}?returnUrl=${encodeURIComponent(returnUrl)}`;
  }

  return basePath;
};

const getLogOutUserUrl = (returnUrl?: string): string => {
  const basePath = '/api/users/logout';

  if (returnUrl) {
    return `${basePath}?returnUrl=${encodeURIComponent(returnUrl)}`;
  }

  return basePath;
};

const getCurrentUser = (signal?: GenericAbortSignal): AxiosPromise<CurrentUserDto> => {
  return callAxios({
    url: `/api/users/current`,
    method: 'GET',
    signal: signal,
  });
};

const redirectTologInUser = (returnUrl?: string): void => {
  const url = getLogInUserUrl(returnUrl);
  window.location.assign(url);
};

const redirectTologOutUser = (returnUrl?: string): void => {
  const url = getLogOutUserUrl(returnUrl);

  window.location.assign(url);
};

export const usersService = {
  getCurrentUser,
  redirectTologInUser,
  redirectTologOutUser,
};
