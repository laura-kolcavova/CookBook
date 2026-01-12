import { useAtom } from 'jotai';
import { tagsAtom } from '../atoms/recipeDataAtom';
import { FormLabel } from '~/pages/shared/forms/FormLabel';
import { FormTagsSelect } from '~/pages/shared/forms/FormTagsSelect';

export const TagsSetter = () => {
  const [tags, setTags] = useAtom(tagsAtom);

  const handleTagsChange = (newTags: string[]) => {
    setTags(newTags);
  };

  return (
    <>
      <FormLabel>Tags</FormLabel>

      <FormTagsSelect tags={tags} onChange={handleTagsChange} />
    </>
  );
};
