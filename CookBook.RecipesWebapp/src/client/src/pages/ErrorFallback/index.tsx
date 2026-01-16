import { FaTriangleExclamation } from 'react-icons/fa6';
import { Alert } from '../shared/Alert';
import { ReactNode } from 'react';
import { FallbackProps, getErrorMessage } from 'react-error-boundary';

export type ErrorFallbackProps = FallbackProps;

export const ErrorFallback = ({ error, resetErrorBoundary }: ErrorFallbackProps): ReactNode => {
  const retry = () => {
    resetErrorBoundary();
  };

  return (
    <div className="min-h-screen flex items-center justify-center px-4 bg-pallete-1">
      <div className="max-w-xl w-full rounded-lg shadow-lg p-8  bg-white">
        <div className="mb-6 text-center">
          <div className="w-16 h-16 bg-red-100 rounded-full flex items-center justify-center mx-auto mb-4">
            <FaTriangleExclamation className="size-10 text-red-600" />
          </div>

          <h1 className="text-2xl font-bold text-gray-900 mb-2">Something went wrong</h1>

          <p className="text-gray-600 mb-4">The application encountered an unexpected error.</p>
        </div>

        <div className="mb-10">
          <Alert color="danger" isDismissible={false}>
            <p>{getErrorMessage(error)}</p>
          </Alert>
        </div>

        <div className="flex flex-col items-center gap-6">
          <button
            className="w-1/2 py-2 px-4 rounded text-white font-bold bg-blue-500 hover:bg-blue-600 focus:outline-none focus:shadow-outline cursor-pointer flex items-center justify-center bg-pallete-4 hover:bg-pallete-5 text-pallete-8"
            onClick={retry}>
            Retry
          </button>
        </div>
      </div>
    </div>
  );
};
