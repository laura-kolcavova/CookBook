import React from 'react';
import { FormLabel } from '~/sharedComponents/forms/FormLabel';
import { FormTextInput } from '~/sharedComponents/forms/FormTextInput';
import { notesAtom } from '../atoms/recipeDataAtom';
import { useAtom } from 'jotai';

export const NotesSetter: React.FC = () => {
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
