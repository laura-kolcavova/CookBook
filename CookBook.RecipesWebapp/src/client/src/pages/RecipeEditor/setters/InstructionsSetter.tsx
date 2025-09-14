import React from 'react';

import { FaPlus, FaTrash } from 'react-icons/fa6';
import type { InstructionItemData } from '~/pages/RecipeEditor/models/InstructionItemData';
import { instructionsAtom } from '../atoms/recipeDataAtom';
import { useAtom } from 'jotai';
import { FormLabel } from '~/sharedComponents/forms/FormLabel';
import { Button } from '~/sharedComponents/Button';
import { FormTextArea } from '~/sharedComponents/forms/FormTextArea';

export const InstructionsSetter: React.FC = () => {
  const [instructions, setInstructions] = useAtom(instructionsAtom);

  const addInstruction = () => {
    const newInstruction: InstructionItemData = {
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
      <FormLabel>Instructions</FormLabel>

      <div className="mb-4">
        {instructions.map((instruction, index) => (
          <div key={index} className="flex flex-row items-center gap-2 mb-4">
            <div className="flex flex-col justify-center h-10">
              <span className="text-base">{index + 1}.</span>
            </div>

            <FormTextArea
              // rows={2}
              placeholder="Describe this step in detail..."
              value={instruction.note}
              onChange={(e) => updateInstruction(index, e.target.value)}
            />

            <Button className="h-10" onClick={() => removeInstruction(index)}>
              <FaTrash size="0.875rem" />
            </Button>
          </div>
        ))}
      </div>

      <Button onClick={addInstruction}>
        <FaPlus className="inline-block align-middle mr-1" />
        <span className="align-middle">Add step</span>
      </Button>

      <div className="mt-2">
        <small>Be specific with temperatures, times, and techniques for best results</small>
      </div>
    </>
  );
};
