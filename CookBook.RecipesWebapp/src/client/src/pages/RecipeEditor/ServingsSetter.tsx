import { useAtom } from 'jotai';
import React from 'react';
import { FormLabel } from '~/sharedComponents/forms/FormLabel';
import { FormExtendedNumberInput } from '~/sharedComponents/forms/FormExtenedNumberInput';
import { servingsAtom } from './atoms/recipeDataAtom';

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
        <small className="text-muted">
          {servings === 1 ? '1 portion' : `${servings} portions`}
        </small>
      </div>
    </>
  );
};
