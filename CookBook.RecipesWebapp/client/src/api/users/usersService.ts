import type { AxiosPromise, GenericAbortSignal } from 'axios';
import { apiClient } from '../apiClient';
import type { CurrentUserDto } from './dto/CurrentUserDto';
import type { ChangeDisplayNameRequestDto } from './dto/ChangeDisplayNameRequestDto';

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
  return apiClient({
    url: `/api/users/current`,
    method: 'GET',
    signal: signal,
  });
};

const changeDisplayName = (
  changeDisplayNameRequest: ChangeDisplayNameRequestDto,
  signal?: GenericAbortSignal,
): AxiosPromise<void> => {
  return apiClient({
    url: '/api/users/current/display-name',
    method: 'PATCH',
    data: changeDisplayNameRequest,
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
  changeDisplayName,
  redirectTologInUser,
  redirectTologOutUser,
};
