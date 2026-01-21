import axios from 'axios';
import { useCallback } from 'react';

export const useSaveRemoveErrorMessage = () => {
  const getErrorMessage = useCallback((error: unknown): string => {
    if (axios.isAxiosError(error) && error.response?.data.code) {
      const code = error.response.data.code;

      return `Something went wrong: ${code}`;
    }

    return 'Something went wrong';
  }, []);

  return { getErrorMessage };
};
