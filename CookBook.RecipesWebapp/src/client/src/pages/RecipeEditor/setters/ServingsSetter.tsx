import { useAtom } from 'jotai';
import React from 'react';
import { servingsAtom } from '../atoms/recipeDataAtom';
import { FormExtendedNumberInput } from '~/pages/shared/forms/FormExtenedNumberInput';
import { FormLabel } from '~/pages/shared/forms/FormLabel';

const MIN: number = 0;
const MAX: number = 255;

export const ServingsSetter: React.FC = () => {
  const [servings, setServings] = useAtom(servingsAtom);

  const handleServingsChange = (newServings: number) => {
    setServings(newServings);
  };

  return (
    <>
      <FormLabel>Number of Servings</FormLabel>

      <FormExtendedNumberInput
        value={servings}
        min={MIN}
        max={MAX}
        onChange={handleServingsChange}
      />

      <div className="mt-1">
        <small>{servings === 1 ? '1 portion' : `${servings} portions`}</small>
      </div>
    </>
  );
};
