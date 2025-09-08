import React, { type PropsWithChildren } from 'react';

export type AlertProps = PropsWithChildren & {
  color: 'info' | 'success' | 'warning' | 'danger';
  isDismissible?: boolean;
};

export const Alert: React.FC<AlertProps> = ({ children }) => {
  return <div>{children}</div>;
};
