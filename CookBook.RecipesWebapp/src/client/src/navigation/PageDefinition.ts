import { FC } from 'react';

export type PageDefinition = {
  paths: string[];
  component: FC;
  public?: boolean;
};
