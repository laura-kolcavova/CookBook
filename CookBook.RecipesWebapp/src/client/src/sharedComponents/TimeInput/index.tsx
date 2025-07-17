import React, { useState, useEffect } from 'react';
import { Input, InputGroup, InputGroupText, ButtonGroup } from 'reactstrap';
import { TimeInputContainer, TimeUnitSelector, PresetButton } from './styled';

interface TimeInputProps {
  value: number; // value in minutes
  onChange: (minutes: number) => void;
  label: string;
  placeholder?: string;
  presets?: { label: string; minutes: number }[];
}

export const TimeInput: React.FC<TimeInputProps> = ({
  value,
  onChange,
  label,
  placeholder,
  presets = [],
}) => {
  const [hours, setHours] = useState(Math.floor(value / 60));
  const [minutes, setMinutes] = useState(value % 60);

  useEffect(() => {
    setHours(Math.floor(value / 60));
    setMinutes(value % 60);
  }, [value]);

  const updateTotalTime = (newHours: number, newMinutes: number) => {
    const totalMinutes = newHours * 60 + newMinutes;
    onChange(totalMinutes);
  };

  const handleHoursChange = (e: React.ChangeEvent<HTMLInputElement>) => {
    const newHours = Math.max(0, parseInt(e.target.value) || 0);
    setHours(newHours);
    updateTotalTime(newHours, minutes);
  };

  const handleMinutesChange = (e: React.ChangeEvent<HTMLInputElement>) => {
    const newMinutes = Math.max(0, Math.min(59, parseInt(e.target.value) || 0));
    setMinutes(newMinutes);
    updateTotalTime(hours, newMinutes);
  };

  const handlePresetClick = (presetMinutes: number) => {
    onChange(presetMinutes);
  };

  return (
    <TimeInputContainer>
      <label>{label}</label>

      <TimeUnitSelector>
        <InputGroup>
          <Input
            type="number"
            min="0"
            max="24"
            placeholder="0"
            value={hours || ''}
            onChange={handleHoursChange}
          />
          <InputGroupText>hours</InputGroupText>

          <Input
            type="number"
            min="0"
            max="59"
            placeholder="0"
            value={minutes || ''}
            onChange={handleMinutesChange}
          />
          <InputGroupText>minutes</InputGroupText>
        </InputGroup>
      </TimeUnitSelector>

      {presets.length > 0 && (
        <ButtonGroup size="sm" className="mt-2">
          {presets.map((preset, index) => (
            <PresetButton
              key={index}
              outline
              color="secondary"
              onClick={() => handlePresetClick(preset.minutes)}
              active={value === preset.minutes}>
              {preset.label}
            </PresetButton>
          ))}
        </ButtonGroup>
      )}

      {placeholder && value === 0 && <small className="text-muted mt-1">{placeholder}</small>}
    </TimeInputContainer>
  );
};
