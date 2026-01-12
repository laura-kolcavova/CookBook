import { useCallback } from 'react';

export const useFormatServings = () => {
  const formatServings = useCallback((servings: number): string => {
    if (servings === 0) {
      return '\u00A0';
    }

    return servings.toString();
  }, []);

  return formatServings;
};
