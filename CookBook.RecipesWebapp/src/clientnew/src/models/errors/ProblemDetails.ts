export type ProblemDetails = {
  type?: string;
  status?: number;
  title?: string;
  detail?: string;
  instance?: string;
  [key: string]: unknown;
};
