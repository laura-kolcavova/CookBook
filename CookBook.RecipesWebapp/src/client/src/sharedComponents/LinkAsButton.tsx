import React from 'react';
import { Link, type LinkProps } from 'react-router-dom';

export type LinkAsButtonProps = LinkProps & {
  variant?: 'primary';
};

export const LinkAsButton: React.FC<LinkAsButtonProps & { className?: string }> = ({
  variant = 'primary',
  className = '',
  ...props
}) => {
  const variantStyles =
    variant === 'primary'
      ? 'button-background-color-primary button-background-color-primary-hover button-color-primary'
      : '';

  return (
    <Link
      className={`px-3 py-2 rounded-md text-base font-normal leading-6 transition-colors duration-150 ${variantStyles} ${className}`}
      {...props}
    />
  );
};
