import { PropsWithChildren } from 'react';

type MetaLabelProps = PropsWithChildren;

export const MetaLabel = ({ children }: MetaLabelProps) => (
  <div className="font-semibold text-gray-900 mb-2 text-sm uppercase tracking-wide">{children}</div>
);
