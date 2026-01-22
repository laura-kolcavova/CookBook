import { descriptionAtom } from '../atoms/recipeDataAtom';
import { useAtom } from 'jotai';
import { FormLabel } from '~/pages/shared/forms/FormLabel';
import { FormTextInput } from '~/pages/shared/forms/FormTextInput';

export const DescriptionSetter = () => {
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
