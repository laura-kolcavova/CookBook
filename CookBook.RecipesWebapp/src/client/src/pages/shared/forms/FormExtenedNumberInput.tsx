import React from 'react';
import { FaMinus, FaPlus } from 'react-icons/fa6';

export type FormExtendedNumberInputProps = {
  value: number;
  min: number;
  max: number;
  onChange: (newValue: number) => void;
  append?: string;
};
export const FormExtendedNumberInput: React.FC<FormExtendedNumberInputProps> = ({
  value,
  min,
  max,
  onChange,
  append = '',
}) => {
  const handleIncrement = () => {
    if (value < max) {
      onChange(value + 1);
    }
  };

  const handleDecrement = () => {
    if (value > min) {
      onChange(value - 1);
    }
  };

  const handleInputChange = (e: React.ChangeEvent<HTMLInputElement>) => {
    const newValue = parseInt(e.target.value) || min;
    const clampedValue = Math.min(max, Math.max(min, newValue));

    onChange(clampedValue);
  };

  return (
    <div className="flex flex-row items-center">
      <button
        onClick={handleDecrement}
        disabled={value <= min}
        className="h-10 px-3 rounded-tl-md rounded-bl-md text-base font-normal transition-colors duration-150 cursor-pointer button-background-color-primary button-background-color-primary-hover button-color-primary">
        <FaMinus size="0.875rem" />
      </button>

      <div className="flex flex-row items-center outline-1 outline-gray-300 mx-[1px] ">
        <input
          type="number"
          min={min}
          max={max}
          value={value}
          onChange={handleInputChange}
          className="w-full h-[calc(2.5rem-2px)] block px-3 py-1.5 text-center text-sm/6 font-normal form-text-input-background-color form-text-input-color"
        />

        {append && (
          <div className="h-[calc(2.5rem-2px)] px-3 text-sm/6 font-normal form-text-input-background-color form-text-input-color flex flex-col justify-center">
            <span>{append}</span>
          </div>
        )}
      </div>

      <button
        onClick={handleIncrement}
        disabled={value >= max}
        className="h-10 px-3 rounded-tr-md rounded-br-md text-base font-normal transition-colors duration-150 cursor-pointer button-background-color-primary button-background-color-primary-hover button-color-primary">
        <FaPlus size="0.875rem" />
      </button>
    </div>
  );
};
