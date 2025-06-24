import { ValidationFailure } from './ValidationFailure';

export type ValidationProblem = {
  type: string;
  status: number;
  title: string;
  instance: string;
  errors: ValidationFailure[];
};
