import type { PropsWithChildren } from 'react';

type MetaItemProps = PropsWithChildren;

export const MetaItem = ({ children }: MetaItemProps) => (
  <div className="bg-gray-100 p-4 rounded-lg text-center">{children}</div>
);
