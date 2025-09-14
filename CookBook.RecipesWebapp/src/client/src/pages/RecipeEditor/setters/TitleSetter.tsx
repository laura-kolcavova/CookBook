import React from 'react';
import { FormLabel } from '~/sharedComponents/forms/FormLabel';
import { FormTextInput } from '~/sharedComponents/forms/FormTextInput';
import { titleAtom } from '../atoms/recipeDataAtom';
import { useAtom } from 'jotai';

export const TitleSetter: React.FC = () => {
  const [title, setTitle] = useAtom(titleAtom);

  return (
    <>
      <FormLabel htmlFor="title">Recipe Title *</FormLabel>

      <FormTextInput
        id="title"
        type="text"
        placeholder="Give your recipe a name"
        value={title}
        onChange={(e) => setTitle(e.target.value)}
        autoComplete="off"
        // invalid={validations.title?.isValid === false}
        required
      />
    </>
  );
};
