import { AxiosError } from 'axios';
import { ProblemDetails } from './ProblemDetails';
import { ValidationProblem } from './ValidationProblem';
import { ErrorCodeProblem } from './ErrorCodeProblem';

export type AxiosGenericError =
  | AxiosError
  | AxiosError<ProblemDetails>
  | AxiosError<ValidationProblem>
  | AxiosError<ErrorCodeProblem>;
