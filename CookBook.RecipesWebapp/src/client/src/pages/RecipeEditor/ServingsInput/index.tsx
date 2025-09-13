import React from 'react';
import { ServingsContainer, ServingsInputGroup, ServingsButton } from './styled';
import { FaMinus, FaPlus } from 'react-icons/fa6';
import { FormTextInput } from '~/sharedComponents/forms/FormTextInput';

const MIN: number = 0;
const MAX: number = 255;

interface ServingsInputProps {
  value: number;
  onChange: (servings: number) => void;
  label: string;
}

export const ServingsInput: React.FC<ServingsInputProps> = ({ value, onChange, label }) => {
  const handleIncrement = () => {
    if (value < MAX) {
      onChange(value + 1);
    }
  };

  const handleDecrement = () => {
    if (value > MIN) {
      onChange(value - 1);
    }
  };

  const handleInputChange = (e: React.ChangeEvent<HTMLInputElement>) => {
    const newValue = parseInt(e.target.value) || MIN;
    const clampedValue = Math.min(MAX, Math.max(MIN, newValue));

    onChange(clampedValue);
  };

  return (
    <ServingsContainer>
      <label>{label}</label>

      <ServingsInputGroup>
        <ServingsButton
          type="button"
          color="outline-secondary"
          onClick={handleDecrement}
          disabled={value <= MIN}>
          <FaMinus />
        </ServingsButton>

        <FormTextInput
          type="number"
          min={MIN}
          max={MAX}
          value={value}
          onChange={handleInputChange}
          className="text-center"
        />

        <ServingsButton
          type="button"
          color="outline-secondary"
          onClick={handleIncrement}
          disabled={value >= MAX}>
          <FaPlus />
        </ServingsButton>
      </ServingsInputGroup>

      <small className="text-muted mt-1">{value === 1 ? '1 portion' : `${value} portions`}</small>
    </ServingsContainer>
  );
};
