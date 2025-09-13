import React, { type ButtonHTMLAttributes } from 'react';

export type ButtonProps = ButtonHTMLAttributes<HTMLButtonElement> & {
  variant?: 'primary';
};

export const Button: React.FC<ButtonProps> = ({
  variant = 'primary',
  className = '',
  ...props
}) => {
  const variantStyles =
    variant === 'primary'
      ? 'button-background-color-primary button-background-color-primary-hover button-color-primary'
      : '';

  return (
    <button
      className={`px-3 py-2 rounded-md text-base font-normal leading-6 transition-colors duration-150 cursor-pointer ${variantStyles} ${className}`}
      {...props}
    />
  );
};
