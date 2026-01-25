import React, { type ButtonHTMLAttributes } from 'react';

export type ButtonProps = ButtonHTMLAttributes<HTMLButtonElement> & {
  variant?: 'primary' | 'danger';
};

export const Button: React.FC<ButtonProps> = ({
  variant = 'primary',
  className = '',
  ...props
}) => {
  let variantStyles: string;

  switch (variant) {
    case 'primary':
      variantStyles =
        'bg-button-background-color-primary text-button-color-primary hover:bg-button-background-color-primary-hover disabled:bg-button-background-color-primary-disabled';
      break;
    case 'danger':
      variantStyles =
        'bg-button-background-color-danger text-button-color-danger hover:bg-button-background-color-danger-hover disabled:bg-button-background-color-danger-disabled';
      break;
    default:
      variantStyles = '';
      break;
  }

  return (
    <button
      type="button"
      className={`px-4 py-2 rounded-md text-base font-normal cursor-pointer transition-colors duration-150 ${variantStyles} ${className}`}
      {...props}
    />
  );
};
