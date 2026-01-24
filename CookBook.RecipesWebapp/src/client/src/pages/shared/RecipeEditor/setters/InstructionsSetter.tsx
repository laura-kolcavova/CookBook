import { FaPlus, FaTrash } from 'react-icons/fa6';
import { FormattedMessage, useIntl } from 'react-intl';

import { instructionsAtom } from '../atoms/recipeDataAtom';
import { useAtom } from 'jotai';
import { FormLabel } from '~/pages/shared/forms/FormLabel';
import { Button } from '~/pages/shared/Button';
import { FormTextArea } from '~/pages/shared/forms/FormTextArea';
import type { RecipeInstructionData } from '../models/RecipeInstructionData';
import { messages } from '../messages';

export const InstructionsSetter = () => {
  const { formatMessage } = useIntl();

  const [instructions, setInstructions] = useAtom(instructionsAtom);

  const addInstruction = () => {
    const newInstruction: RecipeInstructionData = {
      localId: null,
      note: '',
    };

    const newInstructions = [...instructions, newInstruction];

    setInstructions(newInstructions);
  };

  const removeInstruction = (indexToRemove: number) => {
    const newInstructions = instructions.filter((_, index) => index !== indexToRemove);

    setInstructions(newInstructions);
  };

  const updateInstruction = (indexToUpdate: number, note: string) => {
    const newInstructions = instructions.map((instruction, index) =>
      index === indexToUpdate ? { ...instruction, note } : instruction,
    );

    setInstructions(newInstructions);
  };

  return (
    <>
      <FormLabel>
        <FormattedMessage {...messages.instructionsLabel} />
      </FormLabel>

      <div className="mb-4">
        {instructions.map((instruction, index) => (
          <div key={index} className="flex flex-row items-center gap-2 mb-4">
            <div className="flex flex-col justify-center h-10">
              <span className="text-base">{index + 1}.</span>
            </div>

            <FormTextArea
              // rows={2}
              placeholder={formatMessage(messages.instructionPlaceholder)}
              value={instruction.note}
              onChange={(e) => updateInstruction(index, e.target.value)}
              onKeyDown={(e) => {
                if (e.key === 'Enter') {
                  e.preventDefault();
                  addInstruction();
                }
              }}
            />

            <Button className="h-10" onClick={() => removeInstruction(index)}>
              <FaTrash size="0.875rem" />
            </Button>
          </div>
        ))}
      </div>

      <Button onClick={addInstruction} className="flex justify-center items-center">
        <FaPlus className="mr-1" />
        <span>
          <FormattedMessage {...messages.addStepButton} />
        </span>
      </Button>

      <div className="mt-2">
        <small>
          <FormattedMessage {...messages.instructionsHelpText} />
        </small>
      </div>
    </>
  );
};
