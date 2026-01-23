import { useLocalization } from '../LocalizationProvider';
import { useCallback } from 'react';

export const useFormatting = () => {
  const { locale } = useLocalization();

  // const formatMessage = (message: MessageDescriptor): string => {
  //   const translatedMessage = translate(message.id);

  //   return translatedMessage !== undefined ? translatedMessage : message.defaultMessage;
  // };

  const formatDate = useCallback(
    (isoString: string): string => {
      return new Date(isoString).toLocaleDateString(locale);
    },
    [locale],
  );

  const formatTime = useCallback(
    (isoString: string, options?: Intl.DateTimeFormatOptions): string => {
      return new Date(isoString).toLocaleTimeString(locale, options);
    },
    [locale],
  );

  return { formatDate, formatTime };
};
