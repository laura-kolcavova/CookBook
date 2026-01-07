import { AxiosError } from 'axios';
import type { ProblemDetails } from './ProblemDetails';
import type { ValidationProblem } from './ValidationProblem';
import type { ErrorCodeProblem } from './ErrorCodeProblem';

export type AxiosGenericError =
  | AxiosError
  | AxiosError<ProblemDetails>
  | AxiosError<ValidationProblem>
  | AxiosError<ErrorCodeProblem>;
