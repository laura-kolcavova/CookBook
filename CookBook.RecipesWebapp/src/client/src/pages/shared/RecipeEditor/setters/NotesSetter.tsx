import { notesAtom } from '../atoms/recipeDataAtom';
import { FormattedMessage, useIntl } from 'react-intl';
import { useAtom } from 'jotai';
import { FormLabel } from '~/pages/shared/forms/FormLabel';
import { FormTextInput } from '~/pages/shared/forms/FormTextInput';
import { messages } from '../messages';

export const NotesSetter = () => {
  const { formatMessage } = useIntl();

  const [notes, setNotes] = useAtom(notesAtom);

  return (
    <>
      <FormLabel htmlFor="notes">
        <FormattedMessage {...messages.notesLabel} />
      </FormLabel>

      <FormTextInput
        id="notes"
        type="textarea"
        // rows={4}
        placeholder={formatMessage(messages.notesPlaceholder)}
        value={notes || ''}
        onChange={(e) => setNotes(e.target.value || null)}
        autoComplete="off"
      />
    </>
  );
};
