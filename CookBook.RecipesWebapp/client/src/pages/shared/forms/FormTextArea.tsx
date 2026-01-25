import React, { type InputHTMLAttributes } from 'react';

export type FormTextAreaProps = InputHTMLAttributes<HTMLTextAreaElement> & {
  error?: boolean;
};

export const FormTextArea: React.FC<FormTextAreaProps> = ({
  error = false,
  className = '',
  ...props
}) => {
  return (
    <textarea
      className={`block w-full text-sm/6 rounded-md px-3 py-1.5 outline-1 outline-offset-1 outline-gray-300 bg-form-text-input-background-color text-form-text-input-color
        ${error ? '' : ''} ${className}`}
      {...props}
    />
  );
};
