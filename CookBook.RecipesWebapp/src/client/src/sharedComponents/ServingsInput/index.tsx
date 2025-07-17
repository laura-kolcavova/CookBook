import React from 'react';
import { Input, Button } from 'reactstrap';
import { ServingsContainer, ServingsInputGroup, ServingsButton } from './styled';
import { FaMinus, FaPlus } from 'react-icons/fa6';

interface ServingsInputProps {
  value: number;
  onChange: (servings: number) => void;
  label: string;
  min?: number;
  max?: number;
  presets?: number[];
}

export const ServingsInput: React.FC<ServingsInputProps> = ({
  value,
  onChange,
  label,
  min = 1,
  max = 50,
  presets = [1, 2, 4, 6, 8, 12],
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

  const handlePresetClick = (preset: number) => {
    onChange(preset);
  };

  return (
    <ServingsContainer>
      <label>{label}</label>

      <ServingsInputGroup>
        <ServingsButton
          type="button"
          color="outline-secondary"
          onClick={handleDecrement}
          disabled={value <= min}>
          <FaMinus />
        </ServingsButton>

        <Input
          type="number"
          min={min}
          max={max}
          value={value}
          onChange={handleInputChange}
          className="text-center"
        />

        <ServingsButton
          type="button"
          color="outline-secondary"
          onClick={handleIncrement}
          disabled={value >= max}>
          <FaPlus />
        </ServingsButton>
      </ServingsInputGroup>

      <div className="mt-2 d-flex flex-wrap gap-1">
        {presets.map((preset) => (
          <Button
            key={preset}
            size="sm"
            outline
            color="secondary"
            onClick={() => handlePresetClick(preset)}
            active={value === preset}
            className="preset-btn">
            {preset}
          </Button>
        ))}
      </div>

      <small className="text-muted mt-1">{value === 1 ? '1 person' : `${value} people`}</small>
    </ServingsContainer>
  );
};
