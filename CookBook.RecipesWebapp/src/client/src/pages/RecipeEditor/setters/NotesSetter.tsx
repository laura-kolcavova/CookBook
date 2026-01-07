import { notesAtom } from '../atoms/recipeDataAtom';
import { useAtom } from 'jotai';
import { FormLabel } from '~/pages/shared/forms/FormLabel';
import { FormTextInput } from '~/pages/shared/forms/FormTextInput';

export const NotesSetter = () => {
  const [notes, setNotes] = useAtom(notesAtom);

  return (
    <>
      <FormLabel htmlFor="notes">Notes</FormLabel>

      <FormTextInput
        id="notes"
        type="textarea"
        // rows={4}
        placeholder="Any special tips or notes for this recipe..."
        value={notes || ''}
        onChange={(e) => setNotes(e.target.value || undefined)}
        autoComplete="off"
      />
    </>
  );
};
