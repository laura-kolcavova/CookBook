import React, { useMemo } from 'react';
import { Input } from 'reactstrap';
import {
  TimeButton,
  TimeInputContainer,
  TimeInputGroup,
  TimeInputGroupContainer,
  TimeInputGroupText,
} from './styled';
import { FaMinus, FaPlus } from 'react-icons/fa6';
import { getTimeParts, getTotalMinutes } from '~/utils/timeHelper';

interface TimeInputProps {
  valueInMinutes: number;
  onChange: (minutes: number) => void;
  label: string;
}

const MINUTES_MIN = 0;
const MINUTES_MAX = 59;

const HOURS_MIN = 0;
const HOURS_MAX = 168;

export const TimeInput: React.FC<TimeInputProps> = ({ valueInMinutes, onChange, label }) => {
  const { hours, minutes } = useMemo(() => {
    return getTimeParts(valueInMinutes);
  }, [valueInMinutes]);

  const updateTotalTime = (newHours: number, newMinutes: number) => {
    const totalMinutes = getTotalMinutes(newHours, newMinutes);

    onChange(totalMinutes);
  };

  const handleHoursChange = (e: React.ChangeEvent<HTMLInputElement>) => {
    const newHours = parseInt(e.target.value) || HOURS_MIN;
    const clampedValue = Math.min(HOURS_MAX, Math.max(HOURS_MIN, newHours));

    updateTotalTime(clampedValue, minutes);
  };

  const handleHoursIncrement = () => {
    if (hours < HOURS_MAX) {
      updateTotalTime(hours + 1, minutes);
    }
  };

  const handleHoursDecrement = () => {
    if (hours > HOURS_MIN) {
      updateTotalTime(hours - 1, minutes);
    }
  };

  const handleMinutesChange = (e: React.ChangeEvent<HTMLInputElement>) => {
    const newMinutes = parseInt(e.target.value) || MINUTES_MIN;
    const clampedValue = Math.min(MINUTES_MAX, Math.max(MINUTES_MIN, newMinutes));

    updateTotalTime(hours, clampedValue);
  };

  const handleMinutesIncrement = () => {
    if (minutes < MINUTES_MAX) {
      updateTotalTime(hours, minutes + 1);
    }
  };

  const handleMinutesDecrement = () => {
    if (minutes > MINUTES_MIN) {
      updateTotalTime(hours, minutes - 1);
    }
  };

  return (
    <TimeInputContainer>
      <label>{label}</label>

      <TimeInputGroupContainer>
        <TimeInputGroup>
          <TimeButton
            type="button"
            color="outline-secondary"
            onClick={handleHoursDecrement}
            disabled={hours <= HOURS_MIN}>
            <FaMinus />
          </TimeButton>

          <Input
            type="number"
            min={HOURS_MIN}
            max={HOURS_MAX}
            value={hours}
            onChange={handleHoursChange}
          />

          <TimeInputGroupText>h</TimeInputGroupText>

          <TimeButton
            type="button"
            color="outline-secondary"
            onClick={handleHoursIncrement}
            disabled={hours >= HOURS_MAX}>
            <FaPlus />
          </TimeButton>
        </TimeInputGroup>

        <TimeInputGroup>
          <TimeButton
            type="button"
            color="outline-secondary"
            onClick={handleMinutesDecrement}
            disabled={minutes <= MINUTES_MIN}>
            <FaMinus />
          </TimeButton>

          <Input
            type="number"
            min={MINUTES_MIN}
            max={MINUTES_MAX}
            value={minutes}
            onChange={handleMinutesChange}
          />

          <TimeInputGroupText>m</TimeInputGroupText>

          <TimeButton
            type="button"
            color="outline-secondary"
            onClick={handleMinutesIncrement}
            disabled={minutes >= MINUTES_MAX}>
            <FaPlus />
          </TimeButton>
        </TimeInputGroup>
      </TimeInputGroupContainer>
    </TimeInputContainer>
  );
};
