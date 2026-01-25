import { useAtom } from 'jotai';
import { FormattedMessage } from 'react-intl';
import { tagsAtom } from '../atoms/recipeDataAtom';
import { FormLabel } from '~/pages/shared/forms/FormLabel';
import { FormTagsSelect } from '~/pages/shared/forms/FormTagsSelect';
import { messages } from '../messages';

export const TagsSetter = () => {
  const [tags, setTags] = useAtom(tagsAtom);

  const handleTagsChange = (newTags: string[]) => {
    setTags(newTags);
  };

  return (
    <>
      <FormLabel>
        <FormattedMessage {...messages.tagsLabel} />
      </FormLabel>

      <FormTagsSelect tags={tags} onChange={handleTagsChange} />
    </>
  );
};
