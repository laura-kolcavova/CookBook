import { ANTIFORGERY_REQUEST_TOKEN_COOKIE_NAME } from '~/constants';

export const getCsrfToken = (): string | undefined => {
  const cookieKeyName = `${ANTIFORGERY_REQUEST_TOKEN_COOKIE_NAME}=`;

  const csrfToken = document.cookie
    .split('; ')
    .find((row) => row.startsWith(cookieKeyName))
    ?.split('=')[1];

  return csrfToken;
};
