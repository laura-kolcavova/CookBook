import type { PropsWithChildren } from 'react';

type MetaLabelProps = PropsWithChildren;

export const MetaLabel = ({ children }: MetaLabelProps) => (
  <div className="text-sm font-semibold text-gray-900 mb-2 tracking-wide">{children}</div>
);
