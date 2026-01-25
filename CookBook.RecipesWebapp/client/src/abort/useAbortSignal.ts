import { useCallback, useEffect, useRef } from 'react';

export const useAbortSignal = () => {
  const abortController = useRef<AbortController | null>(null);

  const cancelAbortSignal = useCallback(() => {
    if (abortController.current) {
      abortController.current.abort();
      abortController.current = null;
    }
  }, []);

  const createAbortSignal = useCallback((): AbortSignal => {
    cancelAbortSignal();

    abortController.current = new AbortController();

    return abortController.current.signal;
  }, [cancelAbortSignal]);

  const finishAbortSignal = useCallback(() => {
    abortController.current = null;
  }, []);

  useEffect(() => {
    return () => {
      cancelAbortSignal();
    };
  }, [cancelAbortSignal]);

  return { createAbortSignal, cancelAbortSignal, finishAbortSignal };
};
