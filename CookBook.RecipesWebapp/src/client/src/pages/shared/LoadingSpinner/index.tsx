import type { ReactNode } from 'react';
import { SpinnerIcon } from '../icons/SpinnerIcon';

type LoadingSpinnerProps = {
  text?: ReactNode;
};

export const LoadingSpinner = ({ text }: LoadingSpinnerProps) => {
  return (
    <div>
      <div className="mb-2">
        <SpinnerIcon className="animate-spin size-8 mx-auto" />
      </div>

      {text && <span>{text}</span>}
    </div>
  );
};
