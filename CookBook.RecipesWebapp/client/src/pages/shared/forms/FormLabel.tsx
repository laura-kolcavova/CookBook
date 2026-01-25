import React, { type LabelHTMLAttributes } from 'react';

export type FormLabelProps = LabelHTMLAttributes<HTMLLabelElement>;

export const FormLabel: React.FC<FormLabelProps> = ({ children, className = '', ...props }) => {
  return (
    <label
      className={`block text-sm/6 font-medium text-form-label-color mb-2 ${className}`}
      {...props}>
      {children}
    </label>
  );
};
