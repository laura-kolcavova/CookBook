import { type PropsWithChildren, useState } from 'react';
import { FaXmark } from 'react-icons/fa6';

type AlertProps = PropsWithChildren & {
  color: 'info' | 'success' | 'warning' | 'danger';
  isDismissible?: boolean;
};

export const Alert = ({ color, children, isDismissible = false }: AlertProps) => {
  const [visible, setVisible] = useState(true);

  let alertStyles: string;
  let buttonStyles: string;

  switch (color) {
    case 'info':
      alertStyles = 'bg-blue-100 border-blue-500 text-blue-700';
      buttonStyles = 'text-blue-500  hover:text-blue-700';
      break;
    case 'success':
      alertStyles = 'bg-green-100 border-green-500 text-green-700';
      buttonStyles = 'text-green-500 hover:text-green-700';
      break;
    case 'warning':
      alertStyles = 'bg-orange-100 border-orange-500 text-orange-700';
      buttonStyles = 'text-orange-500 hover:text-orange-700';
      break;
    case 'danger':
      alertStyles = 'bg-red-100 border-red-500 text-red-700';
      buttonStyles = 'text-red-500 hover:text-red-700';
      break;
    default:
      alertStyles = '';
      buttonStyles = '';
      break;
  }

  if (!visible) {
    return null;
  }

  return (
    <div className={`p-4 border-l-4 rounded relative mb-4 ${alertStyles}`}>
      {children}
      {isDismissible && (
        <button
          className={`absolute top-0 right-0 p-3 cursor-pointer transition-colors duration-150 ${buttonStyles}`}
          onClick={() => setVisible(false)}>
          <FaXmark size="1.25rem" />
        </button>
      )}
    </div>
  );
};
