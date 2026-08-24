import { useAtom } from 'jotai';
import { FormattedMessage, useIntl } from 'react-intl';
import { titleAtom } from '../atoms/recipeDataAtom';
import { messages } from '../messages';
import { FormLabel } from '~/pages/shared/forms/FormLabel';
import { FormTextInput } from '~/pages/shared/forms/FormTextInput';

export const TitleSetter = () => {
  const { formatMessage } = useIntl();

  const [title, setTitle] = useAtom(titleAtom);

  return (
    <>
      <FormLabel htmlFor="title">
        <FormattedMessage {...messages.recipeTitleLabel} />
      </FormLabel>

      <FormTextInput
        id="title"
        type="text"
        placeholder={formatMessage(messages.recipeTitlePlaceholder)}
        value={title}
        onChange={(e) => setTitle(e.target.value)}
        autoComplete="off"
        // invalid={validations.title?.isValid === false}
        required
      />
    </>
  );
};
