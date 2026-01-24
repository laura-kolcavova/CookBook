import axios from 'axios';
import { useCallback } from 'react';
import { useIntl } from 'react-intl';
import { sharedMessages } from '~/pages/shared/sharedMessages';

export const useRemoveErrorMessage = () => {
  const { formatMessage } = useIntl();

  const getErrorMessage = useCallback(
    (error: Error): string => {
      if (axios.isAxiosError(error) && error.response?.data.code) {
        const code = error.response.data.code;

        return `${formatMessage(sharedMessages.somethingWentWrong)} ${code}`;
      }

      return formatMessage(sharedMessages.somethingWentWrong);
    },
    [formatMessage],
  );

  return { getErrorMessage };
};
