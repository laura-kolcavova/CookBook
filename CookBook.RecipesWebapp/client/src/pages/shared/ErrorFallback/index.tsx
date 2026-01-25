import { FaTriangleExclamation } from 'react-icons/fa6';
import { FormattedMessage } from 'react-intl';

import type { ReactNode } from 'react';
import type { FallbackProps } from 'react-error-boundary';
import { getErrorMessage } from 'react-error-boundary';

import { useNavigate } from 'react-router-dom';
import { pages } from '~/navigation/pages';
import { Alert } from '../Alert';
import { Button } from '../Button';
import { messages } from './messages';

export type ErrorFallbackProps = FallbackProps;

export const ErrorFallback = ({ error, resetErrorBoundary }: ErrorFallbackProps): ReactNode => {
  const navigate = useNavigate();

  const retry = () => {
    resetErrorBoundary();
    navigate(pages.Home.paths[0]);
  };

  return (
    <div className="min-h-screen flex items-center justify-center px-4">
      <div className="max-w-xl w-full rounded-lg shadow-lg p-8 bg-white">
        <div className="mb-6 text-center">
          <div className="w-16 h-16 bg-red-100 rounded-full flex items-center justify-center mx-auto mb-4">
            <FaTriangleExclamation className="size-10 text-red-600" />
          </div>

          <h1 className="text-2xl font-bold text-text-color-primary mb-2">
            <FormattedMessage {...messages.title} />
          </h1>

          <p className="text-base text-text-color-secondary mb-4">
            <FormattedMessage {...messages.description} />
          </p>
        </div>

        <div className="mb-10">
          <Alert color="danger" isDismissible={false}>
            <p>{getErrorMessage(error)}</p>
          </Alert>
        </div>

        <div className="flex flex-col items-center gap-6">
          <Button variant="primary" className="w-1/2" onClick={retry}>
            <FormattedMessage {...messages.retryButton} />
          </Button>
        </div>
      </div>
    </div>
  );
};
