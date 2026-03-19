import type { PropsWithChildren } from 'react';

type MetaItemProps = PropsWithChildren;

export const MetaItem = ({ children }: MetaItemProps) => (
  <div className="p-4 rounded-lg text-center">{children}</div>
);
