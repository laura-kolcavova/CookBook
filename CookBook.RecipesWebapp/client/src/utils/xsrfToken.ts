export const getXsrfToken = (): string | undefined => {
  const xsrfToken = document.cookie
    .split('; ')
    .find((row) => row.startsWith('XSRF-TOKEN='))
    ?.split('=')[1];

  return xsrfToken;
};
