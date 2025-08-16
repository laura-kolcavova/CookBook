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
const HOURS_MAX = 23;

const DAYS_MIN = 0;
const DAYS_MAX = 7;

export const TimeInput: React.FC<TimeInputProps> = ({ valueInMinutes, onChange, label }) => {
  const { days, hours, minutes } = useMemo(() => {
    return getTimeParts(valueInMinutes);
  }, [valueInMinutes]);

  const updateTotalTime = (newDays: number, newHours: number, newMinutes: number) => {
    const totalMinutes = getTotalMinutes(newDays, newHours, newMinutes);

    onChange(totalMinutes);
  };

  const handleDaysChange = (e: React.ChangeEvent<HTMLInputElement>) => {
    const newDays = parseInt(e.target.value) || DAYS_MIN;
    const clampedValue = Math.min(DAYS_MAX, Math.max(DAYS_MIN, newDays));

    updateTotalTime(clampedValue, hours, minutes);
  };

  const handleDaysIncrement = () => {
    if (days < DAYS_MAX) {
      updateTotalTime(days + 1, hours, minutes);
    }
  };

  const handleDaysDecrement = () => {
    if (days > DAYS_MIN) {
      updateTotalTime(days - 1, hours, minutes);
    }
  };

  const handleHoursChange = (e: React.ChangeEvent<HTMLInputElement>) => {
    const newHours = parseInt(e.target.value) || HOURS_MIN;
    const clampedValue = Math.min(HOURS_MAX, Math.max(HOURS_MIN, newHours));

    updateTotalTime(days, clampedValue, minutes);
  };

  const handleHoursIncrement = () => {
    if (hours < HOURS_MAX) {
      updateTotalTime(days, hours + 1, minutes);
    }
  };

  const handleHoursDecrement = () => {
    if (hours > HOURS_MIN) {
      updateTotalTime(days, hours - 1, minutes);
    }
  };

  const handleMinutesChange = (e: React.ChangeEvent<HTMLInputElement>) => {
    const newMinutes = parseInt(e.target.value) || MINUTES_MIN;
    const clampedValue = Math.min(MINUTES_MAX, Math.max(MINUTES_MIN, newMinutes));

    updateTotalTime(days, hours, clampedValue);
  };

  const handleMinutesIncrement = () => {
    if (minutes < MINUTES_MAX) {
      updateTotalTime(days, hours, minutes + 1);
    }
  };

  const handleMinutesDecrement = () => {
    if (minutes > MINUTES_MIN) {
      updateTotalTime(days, hours, minutes - 1);
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
            onClick={handleDaysDecrement}
            disabled={days <= DAYS_MIN}>
            <FaMinus />
          </TimeButton>

          <Input
            type="number"
            min={DAYS_MIN}
            max={DAYS_MAX}
            value={days}
            onChange={handleDaysChange}
          />

          <TimeInputGroupText>d</TimeInputGroupText>

          <TimeButton
            type="button"
            color="outline-secondary"
            onClick={handleDaysIncrement}
            disabled={days >= DAYS_MAX}>
            <FaPlus />
          </TimeButton>
        </TimeInputGroup>

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
