import { descriptionAtom } from '../atoms/recipeDataAtom';
import { FormattedMessage, useIntl } from 'react-intl';
import { useAtom } from 'jotai';
import { FormLabel } from '~/pages/shared/forms/FormLabel';
import { FormTextInput } from '~/pages/shared/forms/FormTextInput';
import { messages } from '../messages';

export const DescriptionSetter = () => {
  const { formatMessage } = useIntl();

  const [description, setDescription] = useAtom(descriptionAtom);

  return (
    <>
      <FormLabel htmlFor="description">
        <FormattedMessage {...messages.descriptionLabel} />
      </FormLabel>

      <FormTextInput
        id="description"
        type="text"
        placeholder={formatMessage(messages.descriptionPlaceholder)}
        value={description || ''}
        onChange={(e) => setDescription(e.target.value || null)}
        autoComplete="off"
      />
    </>
  );
};
