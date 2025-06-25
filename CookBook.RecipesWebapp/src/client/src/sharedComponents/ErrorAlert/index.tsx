import React from 'react';
import axios, { AxiosError } from 'axios';
import { UncontrolledAlert } from 'reactstrap';
import { ErrorCodeProblem } from '~/models/errors/ErrorCodeProblem';
import { ProblemDetails } from '~/models/errors/ProblemDetails';
import { ValidationProblem } from '~/models/errors/ValidationProblem';
import { AxiosGenericError } from '~/models/errors/AxiosGenericError';

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
    return <UncontrolledAlert color="danger">Something went wrong</UncontrolledAlert>;
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
    <UncontrolledAlert color="danger">
      {problem.title}: {problem.detail}: {problem.errorCode}
    </UncontrolledAlert>
  );
};

const ValidaitonProblemAlert: React.FC<{ problem: ValidationProblem }> = ({ problem }) => {
  return <UncontrolledAlert color="danger">{problem.title}</UncontrolledAlert>;
};

const ProblemDetailsAlert: React.FC<{ problem: ProblemDetails }> = ({ problem }) => {
  return <UncontrolledAlert color="danger">{problem.title}</UncontrolledAlert>;
};

const AxiosErrorAlert: React.FC<{ error: AxiosError }> = ({ error }) => {
  return <UncontrolledAlert color="danger">{error.message}</UncontrolledAlert>;
};
