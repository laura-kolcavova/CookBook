import React, { useMemo } from 'react';

import { getTimeParts, getTotalMinutes } from '~/utils/timeHelper';
import { preparationTimeAtom } from '../atoms/recipeDataAtom';
import { useAtom } from 'jotai';
import { FormExtendedNumberInput } from '~/pages/shared/forms/FormExtenedNumberInput';
import { FormLabel } from '~/pages/shared/forms/FormLabel';

const MINUTES_MIN = 0;
const MINUTES_MAX = 59;

const HOURS_MIN = 0;
const HOURS_MAX = 168;

export const PreparationTimeSetter: React.FC = () => {
  const [preparationTime, setPreparationTime] = useAtom(preparationTimeAtom);

  const { hours, minutes } = useMemo(() => {
    return getTimeParts(preparationTime);
  }, [preparationTime]);

  const updateTotalTime = (newHours: number, newMinutes: number) => {
    const totalMinutes = getTotalMinutes(newHours, newMinutes);

    setPreparationTime(totalMinutes);
  };

  const handleHoursChange = (newHours: number) => {
    updateTotalTime(newHours, minutes);
  };

  const handleMinutesChange = (newMinutes: number) => {
    updateTotalTime(hours, newMinutes);
  };

  return (
    <>
      <FormLabel>Preparation Time</FormLabel>

      <div className="flex flex-row gap-4">
        <FormExtendedNumberInput
          value={hours}
          min={HOURS_MIN}
          max={HOURS_MAX}
          append="h"
          onChange={handleHoursChange}
        />

        <FormExtendedNumberInput
          value={minutes}
          min={MINUTES_MIN}
          max={MINUTES_MAX}
          append="m"
          onChange={handleMinutesChange}
        />
      </div>
    </>
  );
};
