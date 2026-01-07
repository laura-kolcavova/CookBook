import { PropsWithChildren } from 'react';

type MetaValueProps = PropsWithChildren;

export const MetaValue = ({ children }: MetaValueProps) => (
  <div className="text-xl text-gray-600">{children}</div>
);
