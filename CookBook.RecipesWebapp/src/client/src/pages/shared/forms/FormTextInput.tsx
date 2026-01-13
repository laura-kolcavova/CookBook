import React, { type InputHTMLAttributes } from 'react';

export type FormTextInputProps = InputHTMLAttributes<HTMLInputElement> & {
  error?: boolean;
};

export const FormTextInput: React.FC<FormTextInputProps> = ({
  error = false,
  className = '',
  ...props
}) => {
  return (
    <input
      className={`block w-full text-sm/6 rounded-md px-3 py-1.5 outline-1 outline-offset-1 outline-gray-300 bg-form-text-input-background-color text-form-text-input-color
        ${error ? '' : ''} ${className}`}
      {...props}
    />
  );
};
