import { titleAtom } from '../atoms/recipeDataAtom';
import { useAtom } from 'jotai';
import { FormLabel } from '~/pages/shared/forms/FormLabel';
import { FormTextInput } from '~/pages/shared/forms/FormTextInput';

export const TitleSetter = () => {
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
