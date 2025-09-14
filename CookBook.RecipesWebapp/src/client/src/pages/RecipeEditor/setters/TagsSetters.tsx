import React from 'react';
import { useAtom } from 'jotai';
import { FormLabel } from '~/sharedComponents/forms/FormLabel';
import { FormTagsSelect } from '~/sharedComponents/forms/FormTagsSelect';
import { tagsAtom } from '../atoms/recipeDataAtom';

export const TagsSetter: React.FC = () => {
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
