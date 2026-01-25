import React from 'react';
import { FaMinus, FaPlus } from 'react-icons/fa6';

export type FormExtendedNumberInputProps = {
  value: number;
  min: number;
  max: number;
  onChange: (newValue: number) => void;
  stepValue?: number;
  append?: string;
};

export const FormExtendedNumberInput = ({
  value,
  min,
  max,
  onChange,
  stepValue = 1,
  append = '',
}: FormExtendedNumberInputProps) => {
  const handleIncrement = () => {
    const diff = value % stepValue;

    const addValue = diff === 0 ? stepValue : stepValue - diff;

    let newValue = value + addValue;

    if (newValue > max) {
      newValue = max;
    }

    onChange(newValue);
  };

  const handleDecrement = () => {
    const diff = value % stepValue;

    const subValue = diff === 0 ? stepValue : diff;

    let newValue = value - subValue;

    if (newValue < min) {
      newValue = min;
    }

    onChange(newValue);
  };

  const handleInputChange = (e: React.ChangeEvent<HTMLInputElement>) => {
    const newValue = parseInt(e.target.value) || min;
    const clampedValue = Math.min(max, Math.max(min, newValue));

    onChange(clampedValue);
  };

  return (
    <div className="flex flex-row items-center">
      <button
        type="button"
        onClick={handleDecrement}
        disabled={value <= min}
        className="h-10 px-3 rounded-tl-md rounded-bl-md text-base font-normal transition-colors duration-150 cursor-pointer bg-button-background-color-primary hover:bg-button-background-color-primary-hover text-button-color-primary">
        <FaMinus size="0.875rem" />
      </button>

      <div className="flex flex-row items-center outline-1 outline-gray-300 mx-[1px] ">
        <input
          step={stepValue}
          type="number"
          min={min}
          max={max}
          value={value}
          onChange={handleInputChange}
          className="w-full h-[calc(2.5rem-1px)] block px-3 py-1.5 text-center text-sm/6 font-normal bg-form-text-input-background-color text-form-text-input-color"
        />

        {append && (
          <div className="h-[calc(2.5rem-2px)] px-3 text-sm/6 font-normal bg-form-text-input-background-color text-form-text-input-color flex flex-col justify-center">
            <span>{append}</span>
          </div>
        )}
      </div>

      <button
        type="button"
        onClick={handleIncrement}
        disabled={value >= max}
        className="h-10 px-3 rounded-tr-md rounded-br-md text-base font-normal transition-colors duration-150 cursor-pointer bg-button-background-color-primary hover:bg-button-background-color-primary-hover text-button-color-primary">
        <FaPlus size="0.875rem" />
      </button>
    </div>
  );
};
