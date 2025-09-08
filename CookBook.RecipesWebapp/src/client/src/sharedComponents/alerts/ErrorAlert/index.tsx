import React from 'react';
import axios, { AxiosError } from 'axios';

import { Alert } from '../Alert';
import type { AxiosGenericError } from '~/models/errors/AxiosGenericError';
import type { ErrorCodeProblem } from '~/models/errors/ErrorCodeProblem';
import type { ValidationProblem } from '~/models/errors/ValidationProblem';
import type { ProblemDetails } from '~/models/errors/ProblemDetails';

interface IErrorAlertProps {
  error: Error | AxiosGenericError;
}

// eslint-disable-next-line @typescript-eslint/no-explicit-any
function isErrorCodeProblem(data: any): data is ErrorCodeProblem {
  return data?.errorCode !== undefined;
}

// eslint-disable-next-line @typescript-eslint/no-explicit-any
function isValidationProblem(data: any): data is ValidationProblem {
  return data?.errors !== undefined;
}

// eslint-disable-next-line @typescript-eslint/no-explicit-any
function isProblemDetails(data: any): data is ProblemDetails {
  return data?.title !== undefined;
}

export const ErrorAlert: React.FC<IErrorAlertProps> = ({ error }) => {
  if (!axios.isAxiosError(error)) {
    return <Alert color="danger">Something went wrong</Alert>;
  }

  const data = error.response?.data;

  if (isErrorCodeProblem(data)) {
    return <ErrorCodeProblemAlert problem={data} />;
  }

  if (isValidationProblem(data)) {
    return <ValidaitonProblemAlert problem={data} />;
  }

  if (isProblemDetails(data)) {
    return <ProblemDetailsAlert problem={data} />;
  }

  return <AxiosErrorAlert error={error} />;
};

const ErrorCodeProblemAlert: React.FC<{ problem: ErrorCodeProblem }> = ({ problem }) => {
  return (
    <Alert color="danger">
      {problem.title}: {problem.detail}: {problem.errorCode}
    </Alert>
  );
};

const ValidaitonProblemAlert: React.FC<{ problem: ValidationProblem }> = ({ problem }) => {
  return <Alert color="danger">{problem.title}</Alert>;
};

const ProblemDetailsAlert: React.FC<{ problem: ProblemDetails }> = ({ problem }) => {
  return <Alert color="danger">{problem.title}</Alert>;
};

const AxiosErrorAlert: React.FC<{ error: AxiosError }> = ({ error }) => {
  return <Alert color="danger">{error.message}</Alert>;
};
