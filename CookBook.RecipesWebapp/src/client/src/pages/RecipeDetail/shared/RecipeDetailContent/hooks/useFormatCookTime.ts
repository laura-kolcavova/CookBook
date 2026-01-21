import { useCallback } from 'react';
import { getTimeParts } from '~/utils/timeHelper';

export const useFormatCookTime = () => {
  const formatCookTime = useCallback((cookTimeInMinutes: number): string => {
    if (cookTimeInMinutes === 0) {
      return '\u00A0';
    }

    const { hours, minutes } = getTimeParts(cookTimeInMinutes);

    if (hours === 0) {
      return `${minutes}min`;
    }

    if (minutes === 0) {
      return `${hours}h`;
    }

    return `${hours}h ${minutes}min`;
  }, []);

  return formatCookTime;
};
