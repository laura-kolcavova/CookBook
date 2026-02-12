import { useCallback } from 'react';
import { useIntl } from 'react-intl';
import { sharedMessages } from '../../sharedMessages';
import { isAxiosError } from 'axios';

export const useSaveRecipeErrorMessage = () => {
  const { formatMessage } = useIntl();

  const getErrorMessage = useCallback(
    (error: Error): string => {
      if (isAxiosError(error) && error.response?.data.errorCode) {
        const code = error.response.data.errorCode;

        return `${formatMessage(sharedMessages.somethingWentWrong)}: ${code}`;
      }

      return formatMessage(sharedMessages.somethingWentWrong);
    },
    [formatMessage],
  );

  return { getErrorMessage };
};
