import { useMemo } from 'react';
import { FormattedMessage } from 'react-intl';

import { getTimeParts, getTotalMinutes } from '~/utils/timeHelper';
import { useAtom } from 'jotai';
import { cookTimeAtom } from '../atoms/recipeDataAtom';
import { FormExtendedNumberInput } from '~/pages/shared/forms/FormExtenedNumberInput';
import { FormLabel } from '~/pages/shared/forms/FormLabel';
import { messages } from '../messages';

const MINUTES_MIN = 0;
const MINUTES_MAX = 60;

const HOURS_MIN = 0;
const HOURS_MAX = 168;

export const CookTimeSetter = () => {
  const [cookTime, setCookTime] = useAtom(cookTimeAtom);

  const { hours, minutes } = useMemo(() => {
    return getTimeParts(cookTime);
  }, [cookTime]);

  const updateTotalTime = (newHours: number, newMinutes: number) => {
    const totalMinutes = getTotalMinutes(newHours, newMinutes);

    setCookTime(totalMinutes);
  };

  const handleHoursChange = (newHours: number) => {
    updateTotalTime(newHours, minutes);
  };

  const handleMinutesChange = (newMinutes: number) => {
    updateTotalTime(hours, newMinutes);
  };

  return (
    <>
      <FormLabel>
        <FormattedMessage {...messages.cookTimeLabel} />
      </FormLabel>

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
          stepValue={5}
        />
      </div>
    </>
  );
};
