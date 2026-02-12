import { isAxiosError } from 'axios';
import { useCallback } from 'react';
import { useIntl } from 'react-intl';
import { sharedMessages } from '~/pages/shared/sharedMessages';
import { messages } from '../messages';

export const useRegisterUserErrorMessage = () => {
  const { formatMessage } = useIntl();

  const getErrorMessage = useCallback(
    (error: Error): string => {
      if (isAxiosError(error) && error.response?.data.code) {
        const code = error.response.data.code;

        switch (code) {
          case 'User.EmailAlreadyExists':
            return formatMessage(messages.emailAlreadyExistsError);
        }

        return `${formatMessage(sharedMessages.somethingWentWrong)} ${code}`;
      }

      return formatMessage(sharedMessages.somethingWentWrong);
    },
    [formatMessage],
  );

  return { getErrorMessage };
};
