import axios from 'axios';
import { appConfig } from '~/config/appConfig';
import { ANTIFORGERY_REQUEST_VERIFICATION_TOKEN_HEADER_NAME, REQUEST_TIMEOUT } from '~/constants';
import { getCsrfToken } from '../utils/csrfToken';

export const apiClient = axios.create({
  baseURL: appConfig.API_URL,
  timeout: REQUEST_TIMEOUT,
  headers: {
    'Content-Type': 'application/json',
    Pragma: 'no-cache',
  },
  // withCredentials: true,
});

apiClient.interceptors.request.use((config) => {
  const csrfToken = getCsrfToken();

  if (csrfToken) {
    config.headers[ANTIFORGERY_REQUEST_VERIFICATION_TOKEN_HEADER_NAME] = csrfToken;
  }

  return config;
});
