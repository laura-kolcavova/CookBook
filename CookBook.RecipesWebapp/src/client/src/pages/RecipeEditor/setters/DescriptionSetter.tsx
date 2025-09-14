import React from 'react';
import { FormLabel } from '~/sharedComponents/forms/FormLabel';
import { FormTextInput } from '~/sharedComponents/forms/FormTextInput';
import { descriptionAtom } from '../atoms/recipeDataAtom';
import { useAtom } from 'jotai';

export const DescriptionSetter: React.FC = () => {
  const [description, setDescription] = useAtom(descriptionAtom);

  return (
    <>
      <FormLabel htmlFor="description">Description</FormLabel>

      <FormTextInput
        id="description"
        type="text"
        placeholder="Introduce your recipe"
        value={description || ''}
        onChange={(e) => setDescription(e.target.value || undefined)}
        autoComplete="off"
      />
    </>
  );
};
