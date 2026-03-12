import { ANTIFORGERY_REQUEST_TOKEN_COOKIE_NAME } from '~/constants';

export const getCsrfToken = (): string | undefined => {
  const csrfToken = document.cookie
    .split('; ')
    .find((row) => row.startsWith(ANTIFORGERY_REQUEST_TOKEN_COOKIE_NAME))
    ?.split('=')[1];

  return csrfToken;
};
