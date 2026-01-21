import type { PropsWithChildren } from 'react';

type MetaValueProps = PropsWithChildren;

export const MetaValue = ({ children }: MetaValueProps) => (
  <div className="text-base text-gray-600">{children}</div>
);
