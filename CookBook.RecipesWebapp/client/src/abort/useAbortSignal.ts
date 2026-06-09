import { useCallback, useEffect, useRef } from 'react';

export const useAbortSignal = () => {
  const abortController = useRef<AbortController | null>(null);

  const abortSignal = useCallback(() => {
    if (abortController.current) {
      abortController.current.abort();
      abortController.current = null;
    }
  }, []);

  const createSignal = useCallback((): AbortSignal => {
    if (abortController.current) {
      abortController.current.abort();
    }

    abortController.current = new AbortController();

    return abortController.current.signal;
  }, []);

  // const getSignal = useCallback((): AbortSignal => {
  //   if (!abortController.current) {
  //     abortController.current = new AbortController();
  //   }

  //   return abortController.current.signal;
  // }, []);

  const finishSignal = useCallback(() => {
    abortController.current = null;
  }, []);

  useEffect(() => {
    return () => {
      abortSignal();
    };
  }, [abortSignal]);

  return { createSignal, abortSignal, finishSignal };
};
